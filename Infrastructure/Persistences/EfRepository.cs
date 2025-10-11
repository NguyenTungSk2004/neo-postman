namespace Infrastructure.Persistence;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.SeedWork;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}

public class EfReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : Entity
{
    public EfReadRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
} 