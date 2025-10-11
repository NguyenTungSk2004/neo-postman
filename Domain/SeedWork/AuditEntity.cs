using Domain.Common.Extensions;

namespace Domain.SeedWork
{
    public abstract class AuditEntity : Entity, ICreationTrackable, IUpdateTrackable
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        protected AuditEntity()
        {
            this.MarkCreated();
        }
    }
}