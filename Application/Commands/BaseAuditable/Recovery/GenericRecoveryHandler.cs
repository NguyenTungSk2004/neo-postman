using MediatR;
using Domain.SeedWork;
using Domain.Common.Extensions;

namespace Application.UseCases.BaseAuditable.Recovery
{
    public abstract class GenericRecoveryHandler<TEntity, TCommand> : IRequestHandler<TCommand, bool>
        where TEntity : Entity, ISoftDeletable, IAggregateRoot
        where TCommand : GenericRecoveryCommand
    {
        private readonly IRepository<TEntity> _repository;
        protected GenericRecoveryHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId != 1)
                    throw new UnauthorizedAccessException("User không có quyền khôi phục bản ghi");

                var record = await _repository.GetByIdAsync(
                    request.Id,
                    cancellationToken
                ) ?? throw new KeyNotFoundException($"Không tìm thấy bản ghi.");

                if (!record.IsDeleted)
                    throw new ApplicationException("Không thể khôi phục bản ghi chưa bị xóa");

                record.Recover();
                await _repository.UpdateAsync(record, cancellationToken);
                return true;
            }
            catch (KeyNotFoundException knfEx)
            {
                throw new KeyNotFoundException(knfEx.Message);
            }
            catch (UnauthorizedAccessException uaeEx)
            {
                throw new UnauthorizedAccessException(uaeEx.Message);
            }
            catch (ApplicationException ehEx)
            {
                throw new ApplicationException(ehEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi khôi phục bản ghi: {ex.Message}");
            }
        }
    }
}