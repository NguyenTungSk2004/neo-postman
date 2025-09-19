namespace SharedKernel.Interfaces;

public interface ICreationTrackable
{
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public interface IUpdateTrackable
{
    public long? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public interface ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}
