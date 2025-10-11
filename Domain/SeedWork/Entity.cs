using Yitter.IdGenerator;

namespace Domain.SeedWork
{
    public abstract class Entity : IEquatable<Entity>
    {
        private int? _requestedHashCode;
        public long Id { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected Entity()
        {
            Id = YitIdHelper.NextId();
        }

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public override bool Equals(object? obj) => Equals(obj as Entity);
        public bool Equals(Entity? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Id == other.Id && this.GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            _requestedHashCode ??= (Id.GetHashCode() ^ 31);
            return _requestedHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right) => Equals(left, right);
        public static bool operator !=(Entity left, Entity right) => !(left == right);
    }
}