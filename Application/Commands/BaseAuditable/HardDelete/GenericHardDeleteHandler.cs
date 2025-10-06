using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.SeedWork;
using Domain.Specifications;
using SharedKernel.SeedWork;

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
                    throw new ExceptionHelper("Không tìm thấy bất kỳ bản ghi nào");

                await _repository.DeleteRangeAsync(toDelete, cancellationToken);
                return true;
            }
            catch (DbUpdateException ex)
            {
                string errorMessage = ex.InnerException?.Message ?? ex.Message;

                if (errorMessage.Contains("REFERENCE constraint", StringComparison.OrdinalIgnoreCase))
                {
                    throw new ExceptionHelper($"Không thể xóa vì thông tin bản ghi đang được sử dụng.");
                }

                throw new ExceptionHelper($"Đã xảy ra lỗi khi xóa.");
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (ExceptionHelper ex)
            {
                throw new ExceptionHelper(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Có lỗi khi thực thi: {ex.Message}");
            }
        }
    }
}