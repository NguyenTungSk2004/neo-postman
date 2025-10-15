using Ardalis.Specification.EntityFrameworkCore;
using Domain.SeedWork;

namespace Infrastructure.Persistence;
public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(AppDbContext dbContext) : base(dbContext){}
    public void Add(T entity) => DbContext.Set<T>().Add(entity);
    public void Update(T entity) => DbContext.Set<T>().Update(entity);
    public void Delete(T entity) => DbContext.Set<T>().Remove(entity);
}

public class EfReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : Entity
{
    public EfReadRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
} 