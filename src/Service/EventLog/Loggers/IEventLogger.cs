using Passwordless.Common.EventLog.Enums;
using Passwordless.Common.Extensions;
using Passwordless.Common.Models;
using Passwordless.Service.EventLog.Models;

namespace Passwordless.Service.EventLog.Loggers;

public interface IEventLogger
{
    void LogEvent(EventDto @event);
    void LogEvent(Func<IEventLogContext, EventDto> eventFunc);
    Task FlushAsync();
}

public static class EventLoggerExtensions
{
    public static void LogRegistrationTokenCreatedEvent(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Created registration token for {performedBy}",
            Severity = Severity.Informational,
            EventType = EventType.ApiAuthUserRegistered,
            PerformedAt = context.PerformedAt,
            PerformedBy = performedBy,
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogSigninTokenCreatedEvent(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Created signin token for {performedBy}",
            Severity = Severity.Informational,
            EventType = EventType.ApiUserSigninTokenCreated,
            PerformedAt = context.PerformedAt,
            PerformedBy = performedBy,
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogMagicLinkCreatedEvent(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Created signin token for {performedBy}",
            Severity = Severity.Informational,
            EventType = EventType.ApiUserMagicLinkCreated,
            PerformedAt = context.PerformedAt,
            PerformedBy = performedBy,
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogRegistrationBeganEvent(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Beginning passkey registration for {performedBy}",
            PerformedBy = performedBy,
            PerformedAt = context.PerformedAt,
            EventType = EventType.ApiAuthPasskeyRegistrationBegan,
            Severity = Severity.Informational,
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogRegistrationCompletedEvent(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Completed passkey registration for user {performedBy}",
            PerformedBy = performedBy,
            PerformedAt = context.PerformedAt,
            EventType = EventType.ApiAuthPasskeyRegistrationCompleted,
            Severity = Severity.Informational,
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogUserAliasSetEvent(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Added set aliases for user ({performedBy}).",
            PerformedAt = context.PerformedAt,
            PerformedBy = performedBy,
            TenantId = context.TenantId,
            EventType = EventType.ApiUserSetAliases,
            Severity = Severity.Informational,
            Subject = performedBy,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogApplicationCreatedEvent(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = $"{context.TenantId} created.",
            PerformedBy = performedBy,
            TenantId = context.TenantId,
            EventType = EventType.ApiManagementAppCreated,
            Severity = Severity.Informational,
            Subject = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogAppFrozenEvent(this IEventLogger logger) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "Application frozen.",
            PerformedBy = "System",
            TenantId = context.TenantId,
            EventType = EventType.ApiManagementAppFrozen,
            Severity = Severity.Alert,
            Subject = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogAppUnfrozenEvent(this IEventLogger logger) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "Application unfrozen.",
            PerformedBy = "System",
            TenantId = context.TenantId,
            EventType = EventType.ApiManagementAppUnfrozen,
            Severity = Severity.Alert,
            Subject = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogAppMarkedToDeleteEvent(this IEventLogger logger, string deletedBy) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "Application was marked for deletion.",
            PerformedBy = deletedBy,
            TenantId = context.TenantId,
            EventType = EventType.ApiManagementAppMarkedForDeletion,
            Severity = Severity.Informational,
            Subject = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogAppDeleteCancelledEvent(this IEventLogger logger) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "Application deletion was cancelled.",
            PerformedBy = "System",
            TenantId = context.TenantId,
            EventType = EventType.ApiManagementAppDeletionCancelled,
            Severity = Severity.Informational,
            Subject = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogDeleteCredentialEvent(this IEventLogger logger, string userId) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = $"Deleted credential for user {userId}",
            PerformedBy = "System",
            TenantId = context.TenantId,
            EventType = EventType.ApiUserDeleteCredential,
            Severity = Severity.Informational,
            Subject = userId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogUserSignInCompletedEvent(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = $"User {performedBy} completed sign in.",
            PerformedBy = performedBy,
            TenantId = context.TenantId,
            EventType = EventType.ApiUserSignInCompleted,
            Severity = Severity.Informational,
            Subject = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogUserSignInTokenVerifiedEvent(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = $"User {performedBy} verified sign in token.",
            PerformedBy = performedBy,
            TenantId = context.TenantId,
            EventType = EventType.ApiUserSignInVerified,
            Severity = Severity.Informational,
            Subject = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogDeletedUserEvent(this IEventLogger logger, string userId) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = $"Deleted user {userId}",
            PerformedBy = "System",
            TenantId = context.TenantId,
            EventType = EventType.ApiUserDeleted,
            Severity = Severity.Informational,
            Subject = userId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogApiKeyCreatedEvent(this IEventLogger logger, string apiKey) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Created Api Key {apiKey.GetLast(4)}",
            Severity = Severity.Informational,
            EventType = EventType.AdminApiKeyCreated,
            PerformedAt = context.PerformedAt,
            PerformedBy = "Unknown User",
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogApiKeyLockedEvent(this IEventLogger logger, string apiKeyId) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Locked Api Key {apiKeyId}",
            Severity = Severity.Informational,
            EventType = EventType.AdminApiKeyLocked,
            PerformedAt = context.PerformedAt,
            PerformedBy = "Unknown User",
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogApiKeyUnlockedEvent(this IEventLogger logger, string apiKeyId) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Unlocked Api Key {apiKeyId}",
            Severity = Severity.Informational,
            EventType = EventType.AdminApiKeyUnlocked,
            PerformedAt = context.PerformedAt,
            PerformedBy = "Unknown User",
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogApiKeyDeletedEvent(this IEventLogger logger, string apiKeyId) =>
        logger.LogEvent(context => new EventDto
        {
            Message = $"Deleted Api Key {apiKeyId}",
            Severity = Severity.Informational,
            EventType = EventType.AdminApiKeyDeleted,
            PerformedAt = context.PerformedAt,
            PerformedBy = "Unknown User",
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogApiKeysEnumeratedEvent(this IEventLogger logger) =>
        logger.LogEvent(context => new EventDto
        {
            Message = "Api keys enumerated",
            Severity = Severity.Informational,
            EventType = EventType.AdminApiKeysEnumerated,
            PerformedAt = context.PerformedAt,
            PerformedBy = "Unknown User",
            Subject = context.TenantId,
            TenantId = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogInvalidApiSecretUsedEvent(this IEventLogger logger, DateTime performedAt, string tenantId, ApplicationSecretKey secretKey) =>
        logger.LogEvent(new EventDto
        {
            PerformedAt = performedAt,
            Message = "A request using an invalid ApiSecret was attempted.",
            PerformedBy = "Unknown User",
            TenantId = tenantId,
            EventType = EventType.ApiAuthInvalidSecretKeyUsed,
            Severity = Severity.Alert,
            Subject = tenantId,
            ApiKeyId = secretKey.AbbreviatedValue
        });

    public static void LogInvalidPublicKeyUsedEvent(this IEventLogger logger, DateTime performedAt, string tenantId, ApplicationPublicKey publicKey) =>
        logger.LogEvent(new EventDto
        {
            PerformedAt = performedAt,
            Message = "A request using an invalid PublicKey was attempted.",
            PerformedBy = "Unknown User",
            TenantId = tenantId,
            EventType = EventType.ApiAuthInvalidPublicKeyUsed,
            Severity = Severity.Alert,
            Subject = tenantId,
            ApiKeyId = publicKey.AbbreviatedValue
        });

    public static void LogDisabledApiKeyUsedEvent(this IEventLogger logger, DateTime performedAt, string tenantId, ApplicationSecretKey secretKey) =>
        logger.LogEvent(new EventDto
        {
            PerformedAt = performedAt,
            Message = "Disabled Api Key used.",
            PerformedBy = "Unknown User",
            TenantId = tenantId,
            EventType = EventType.ApiAuthDisabledSecretKeyUsed,
            Severity = Severity.Alert,
            Subject = tenantId,
            ApiKeyId = secretKey.AbbreviatedValue
        });

    public static void LogDisabledPublicKeyUsedEvent(this IEventLogger logger, DateTime performedAt, string tenantId, ApplicationPublicKey publicKey) =>
        logger.LogEvent(new EventDto
        {
            PerformedAt = performedAt,
            Message = "Disabled Public Key used.",
            PerformedBy = "Unknown User",
            TenantId = tenantId,
            EventType = EventType.ApiAuthDisabledPublicKeyUsed,
            Severity = Severity.Alert,
            Subject = tenantId,
            ApiKeyId = publicKey.AbbreviatedValue
        });

    public static void LogGenerateSignInTokenEndpointEnabled(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "/signin/generate-token was enabled.",
            PerformedBy = string.IsNullOrWhiteSpace(performedBy) ? "Unknown User" : performedBy,
            TenantId = context.TenantId,
            EventType = EventType.AdminGenerateSignInTokenEndpointEnabled,
            Severity = Severity.Alert,
            Subject = context.TenantId,
            ApiKeyId = string.Empty
        });

    public static void LogGenerateSignInTokenEndpointDisabled(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "/signin/generate-token endpoint was disabled.",
            PerformedBy = string.IsNullOrWhiteSpace(performedBy) ? "Unknown User" : performedBy,
            TenantId = context.TenantId,
            EventType = EventType.AdminGenerateSignInTokenEndpointDisabled,
            Severity = Severity.Alert,
            Subject = context.TenantId,
            ApiKeyId = string.Empty
        });

    public static void LogAuthenticatorsWhitelistedEvent(this IEventLogger logger) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "Authenticators whitelisted.",
            PerformedBy = "Unknown User",
            TenantId = context.TenantId,
            EventType = EventType.ApiAuthenticatorsWhitelisted,
            Severity = Severity.Informational,
            Subject = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogAuthenticatorsDelistedEvent(this IEventLogger logger) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "Authenticators removed from whitelist or blacklist.",
            PerformedBy = "Unknown User",
            TenantId = context.TenantId,
            EventType = EventType.ApiAuthenticatorsDelisted,
            Severity = Severity.Informational,
            Subject = context.TenantId,
            ApiKeyId = context.AbbreviatedKey
        });

    public static void LogMagicLinksEnabled(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "Magic Links feature was enabled.",
            PerformedBy = string.IsNullOrWhiteSpace(performedBy) ? "Unknown User" : performedBy,
            TenantId = context.TenantId,
            EventType = EventType.AdminMagicLinksEnabled,
            Severity = Severity.Alert,
            Subject = context.TenantId,
            ApiKeyId = string.Empty
        });

    public static void LogMagicLinksDisabled(this IEventLogger logger, string performedBy) =>
        logger.LogEvent(context => new EventDto
        {
            PerformedAt = context.PerformedAt,
            Message = "Magic Links feature was disabled.",
            PerformedBy = string.IsNullOrWhiteSpace(performedBy) ? "Unknown User" : performedBy,
            TenantId = context.TenantId,
            EventType = EventType.AdminMagicLinksDisabled,
            Severity = Severity.Alert,
            Subject = context.TenantId,
            ApiKeyId = string.Empty
        });
}