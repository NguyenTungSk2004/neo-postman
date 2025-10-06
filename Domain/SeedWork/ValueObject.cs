namespace Domain.SeedWork;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }
        return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right) => !(EqualOperator(left, right));

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj) => Equals(obj as ValueObject);
    public bool Equals(ValueObject? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other == null || other.GetType() != GetType()) return false;
        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public ValueObject GetCopy() => (ValueObject)this.MemberwiseClone();
}
