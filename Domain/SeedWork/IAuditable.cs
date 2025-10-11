namespace Domain.SeedWork;

public interface ICreationTrackable
{
    public DateTimeOffset CreatedAt { get; set; }
}
public interface IUpdateTrackable
{
    public DateTimeOffset? UpdatedAt { get; set; }
}
public interface IExpirable
{
    public DateTimeOffset ExpiresAt { get; set; }
}
public interface ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
