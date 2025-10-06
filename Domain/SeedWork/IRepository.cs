using Ardalis.Specification;
namespace Domain.SeedWork;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : Entity
{
}
