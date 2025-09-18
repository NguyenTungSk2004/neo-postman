using Ardalis.Specification;
using SharedKernel.Base;

namespace SharedKernel.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : Entity
{
}
