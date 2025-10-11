using MediatR;
using Domain.SeedWork;
using Domain.Common.Specifications;

namespace Application.UseCases.BaseAuditable.HardDelete
{
    public abstract class GenericHardDeleteHandler<TEntity, TCommand> : IRequestHandler<TCommand, bool>
        where TEntity : Entity, ISoftDeletable, IAggregateRoot
        where TCommand : GenericHardDeleteCommand
    {
        private readonly IRepository<TEntity> _repository;

        protected GenericHardDeleteHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId != 1)
                    throw new UnauthorizedAccessException("User không có quyền xóa vĩnh viễn bản ghi");

                var spec = new EntitiesByIdsSpecification<TEntity>(request.Ids, true);
                var entities = await _repository.ListAsync(spec, cancellationToken);
                var toDelete = entities.Where(e => (bool)(e.GetType().GetProperty("IsDeleted")?.GetValue(e) ?? false)).ToList();

                if (toDelete == null || toDelete.Count == 0)
                    throw new ApplicationException("Không tìm thấy bất kỳ bản ghi nào");

                await _repository.DeleteRangeAsync(toDelete, cancellationToken);
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Có lỗi khi thực thi: {ex.Message}");
            }
        }
    }
}