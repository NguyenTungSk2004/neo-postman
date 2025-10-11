using Domain.SeedWork;

namespace Domain.Common.Extensions;
public static class ExpiryExtensions
{
    public static void MarkExpiredIn(this IExpirable entity, TimeSpan duration)
    {
        entity.ExpiresAt = DateTimeOffset.UtcNow.Add(duration);
    }
    public static void MarkExpiredNow(this IExpirable entity)
    {
        entity.ExpiresAt = DateTimeOffset.UtcNow;
    }
    public static void MarkExpiredAt(this IExpirable entity, DateTimeOffset expiredAt)
    {
        entity.ExpiresAt = expiredAt;
    }
    public static bool IsExpired(this IExpirable entity)
        => entity.ExpiresAt > DateTimeOffset.UtcNow;
}