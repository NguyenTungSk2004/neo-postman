using SharedKernel.Interfaces;

namespace SharedKernel.Base;
public static class AuditExtensions
{ 
    public static void MarkCreated(this ICreationTrackable entity, long userId)
    {
        entity.CreatedBy = userId;
        entity.CreatedAt = DateTime.UtcNow;
    }

    public static void MarkUpdated(this IUpdateTrackable entity, long userId)
    {
        entity.UpdatedBy = userId;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public static void MarkDeleted(this ISoftDeletable entity, long userId)
    {
        entity.IsDeleted = true;
        entity.DeletedBy = userId;
        entity.DeletedAt = DateTime.UtcNow;
    }

    public static void Recover(this ISoftDeletable entity)
    {
        entity.IsDeleted = false;
        entity.DeletedBy = null;
        entity.DeletedAt = null;
    }
}
