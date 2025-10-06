using Domain.SeedWork;

namespace Domain.Common.Extensions;
public static class ExpiryExtensions
{
    public static void MarkExpired(this IExpirable entity, DateTimeOffset now)
    {
        entity.ExpiresAt = now;
    }

    public static void MarkExpiredAt(this IExpirable entity, DateTimeOffset expiredAt)
    {
        entity.ExpiresAt = expiredAt;
    }

    public static void ClearExpiry(this IExpirable entity)
    {
        entity.ExpiresAt = null;
    }

    public static bool IsExpired(this IExpirable entity, DateTimeOffset now)
        => entity.ExpiresAt.HasValue && entity.ExpiresAt.Value <= now;
}