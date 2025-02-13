using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Passwordless.Service.Storage.Ef;

namespace Passwordless.Service.EventLog;

public class EventDeletionBackgroundWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISystemClock _systemClock;
    private readonly ILogger<EventDeletionBackgroundWorker> _logger;

    public EventDeletionBackgroundWorker(IServiceProvider serviceProvider, ISystemClock systemClock, ILogger<EventDeletionBackgroundWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _systemClock = systemClock;
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting Event Log Deletion Worker");

        using PeriodicTimer timer = new(TimeSpan.FromDays(1));

        try
        {
            do
            {
                await DeleteExpiredEventLogs(stoppingToken);
            } while (await timer.WaitForNextTickAsync(stoppingToken));
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Event Log Deletion Worker was cancelled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Event Log Deletion failed.");
        }
    }

    private async Task DeleteExpiredEventLogs(CancellationToken cancellationToken)
    {
        using var serviceScope = _serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DbGlobalContext>();

        var eventsToDelete = await dbContext.ApplicationEvents
            .Where(x => x.PerformedAt <= _systemClock.UtcNow.UtcDateTime.AddDays(-x.Application.Features.EventLoggingRetentionPeriod))
            .ExecuteDeleteAsync(cancellationToken);

        _logger.LogInformation("{eventsDeleted} events deleted from EventLogging", eventsToDelete);
    }
}