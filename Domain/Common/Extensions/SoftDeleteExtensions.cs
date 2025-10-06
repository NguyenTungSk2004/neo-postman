using Domain.SeedWork;

namespace Domain.Common.Extensions;
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
