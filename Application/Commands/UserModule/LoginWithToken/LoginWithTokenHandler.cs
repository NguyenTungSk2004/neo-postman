using Domain.AggregatesModel.SessionAggregate;
using Domain.AggregatesModel.SessionAggregate.Specifications;
using Domain.AggregatesModel.UserAggregate;
using Domain.Common.Utilities;
using Domain.SeedWork;
using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.LoginWithToken
{
    public class LoginWithTokenHandler(
        IRepository<UserSession> userSessionRepository,
        IRepository<User> userRepository)
        : IRequestHandler<LoginWithTokenCommand, Result>
    {
        public async Task<Result> Handle(LoginWithTokenCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSessionSpecification.ByRefreshTokenHash(
                request.DeviceInfo,
                request.IpAddress,
                HashHelper.HashToken(request.RefreshToken)
            );

            var userSession = await userSessionRepository.FirstOrDefaultAsync(spec, cancellationToken);
            if (userSession is null)
                return Result.Failure("Invalid or expired refresh token.");

            var user = await userRepository.GetByIdAsync(userSession.UserId, cancellationToken);
            if (user is null)
                return Result.Failure("User not found.");

            await userSessionRepository.UpdateAsync(userSession, cancellationToken);

            return Result.Success();
        }
    }
}