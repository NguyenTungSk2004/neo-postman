using Ardalis.Specification;
namespace Domain.SeedWork;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(T entity);
}

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : Entity
{
}
