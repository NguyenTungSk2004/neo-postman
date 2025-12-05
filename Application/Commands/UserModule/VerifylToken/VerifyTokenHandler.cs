using MediatR;
using SharedKernel.Common;
using Domain.AggregatesModel.VerificationAggregate;
using Domain.SeedWork;
using Domain.AggregatesModel.VerificationAggregate.Specifications;

namespace Application.Commands.UserModule.VerifyToken
{
    public class VerifyTokenHandler: IRequestHandler<VerifyTokenCommand, Result>
    {
        private readonly IRepository<UserVerificationToken> _userVerificationTokenRepository;
        public VerifyTokenHandler(IRepository<UserVerificationToken> userVerificationTokenRepository)
        {
            _userVerificationTokenRepository = userVerificationTokenRepository;
        }
        public async Task<Result> Handle(VerifyTokenCommand request, CancellationToken cancellationToken)
        {
            var spec = UserVerificationTokenSpecification.ByToken(request.Token, request.Type);
            var tokenEntity =  await _userVerificationTokenRepository.FirstOrDefaultAsync(spec, cancellationToken);
            if (tokenEntity is null)
                return Result.Failure("Invalid or expired verification token.");

            tokenEntity.MarkAsUsed();
            if (request.Type == TypeOfVerificationToken.EmailVerification)
            {
                tokenEntity.User.MarkEmailAsVerified();
            }
            await _userVerificationTokenRepository.UpdateAsync(tokenEntity, cancellationToken);

            return Result.Success();
        }
    }    
}