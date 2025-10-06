using MediatR;
using SharedKernel.Exceptions;
using Domain.SeedWork;
using Domain.Specifications;
using SharedKernel.SeedWork;

namespace Application.UseCases.BaseAuditable.SoftDelete
{
    public abstract class GenericSoftDeleteHandler<TEntity, TCommand> : IRequestHandler<TCommand, bool>
         where TEntity : Entity, ISoftDeletable, IAggregateRoot
         where TCommand : GenericSoftDeleteCommand
    {
        private readonly IRepository<TEntity> _repository;

        protected GenericSoftDeleteHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var spec = new EntitiesByIdsSpecification<TEntity>(request.Ids, false);
                var entities = await _repository.ListAsync(spec, cancellationToken);
                if (entities == null || entities.Count == 0)
                    throw new ExceptionHelper("Không tìm thấy bất kỳ bản ghi nào");

                foreach (var entity in entities)
                {
                    entity.MarkDeleted(request.UserId);
                }

                await _repository.UpdateRangeAsync(entities, cancellationToken);
                return true;
            }
            catch (ExceptionHelper ex)
            {
                throw new ExceptionHelper(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi xóa bản ghi: {ex.Message}");
            }
        }
    }
}
