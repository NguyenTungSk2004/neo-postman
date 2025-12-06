using Domain.AggregatesModel.VerificationAggregate;
using Domain.AggregatesModel.VerificationAggregate.Specifications;
using Domain.SeedWork;
using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.VerifyToken
{
    public class VerifyTokenHandler(
        IRepository<UserVerificationToken> userVerificationTokenRepository
    ) : IRequestHandler<VerifyTokenCommand, Result>
    {
        public async Task<Result> Handle(VerifyTokenCommand request, CancellationToken cancellationToken)
        {
            var spec = UserVerificationTokenSpecification.ByToken(request.Token, request.Type);
            var tokenEntity = await userVerificationTokenRepository.FirstOrDefaultAsync(spec, cancellationToken);
            if (tokenEntity is null)
                return Result.Failure("Invalid or expired verification token.");

            tokenEntity.MarkAsUsed();
            if (request.Type == TypeOfVerificationToken.EmailVerification)
            {
                tokenEntity.User.MarkEmailAsVerified();
            }
            await userVerificationTokenRepository.UpdateAsync(tokenEntity, cancellationToken);

            return Result.Success();
        }
    }
}