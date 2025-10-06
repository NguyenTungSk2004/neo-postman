using Yitter.IdGenerator;

namespace Domain.SeedWork
{
    public abstract class Entity : IEquatable<Entity>
    {
        private int? _requestedHashCode;
        public long Id { get; protected set; }

        protected Entity()
        {
            Id = YitIdHelper.NextId();
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