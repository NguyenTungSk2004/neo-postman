namespace SharedKernel.SeedWork;
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
public static class AuditExtensions 
{
    public static void MarkCreated(this ICreationTrackable entity)
    {
        entity.CreatedAt = DateTimeOffset.UtcNow;
    }

    public static void MarkUpdated(this IUpdateTrackable entity)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;
    }
}
public static class SoftDeleteExtensions
{
    public static void MarkDeleted(this ISoftDeletable entity, long? deletedBy)
    {
        entity.IsDeleted = true;
        entity.DeletedBy = deletedBy;
        entity.DeletedAt = DateTimeOffset.UtcNow;
    }

    public static void Recover(this ISoftDeletable entity)
    {
        entity.IsDeleted = false;
        entity.DeletedBy = null;
        entity.DeletedAt = null;
    }
}
