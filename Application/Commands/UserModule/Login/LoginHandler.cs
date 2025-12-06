using Application.Common.Interfaces;
using Domain.AggregatesModel.SessionAggregate;
using Domain.AggregatesModel.UserAggregate;
using Domain.AggregatesModel.UserAggregate.Specifications;
using Domain.AggregatesModel.VerificationAggregate;
using Domain.AggregatesModel.VerificationAggregate.Specifications;
using Domain.SeedWork;
using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.Login
{
    public class LoginHandler(
        IRepository<User> userRepository,
        IRepository<UserSession> userSessionRepository,
        IRepository<UserVerificationToken> userVerificationTokenRepository,
        IPasswordHasher passwordHasher
    ) : IRequestHandler<LoginCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var spec = new UserByEmailSpecification(request.Email);
            var user = await userRepository.FirstOrDefaultAsync(spec);
            if (user is null)
                return Result<string>.Failure("Email is not registered. Please sign up first.");

            if (user.EmailVerifiedAt is null)
            {
                var specToken = UserVerificationTokenSpecification.ByUserId(user.Id, TypeOfVerificationToken.EmailVerification);
                var existingToken = await userVerificationTokenRepository.AnyAsync(specToken);
                if (!existingToken)
                {
                    var token = UserVerificationToken.GenerateToken(user.Id, TypeOfVerificationToken.EmailVerification, TimeSpan.FromHours(1));
                    await userVerificationTokenRepository.AddAsync(token, cancellationToken);
                }
                return Result<string>.Failure("Email is not verified. Please verify your email before logging in.");
            }

            if (user.IsDisabled)
                return Result<string>.Failure("User account is disabled. Please contact admin.");

            var localAccount = user.UserAuthProviders.FirstOrDefault(x => x.Provider == AuthProvider.Local);
            if (localAccount is null)
                return Result<string>.Failure("Local account is not set up for this user. Please use external login.");

            if (!passwordHasher.VerifyPassword(request.Password, localAccount.PasswordHash!))
                return Result<string>.Failure("Password is incorrect. Please try again.");

            var session = UserSession.CreateNewSession(user.Id, request.DeviceInfo, request.IpAddress);
            await userSessionRepository.AddAsync(session, cancellationToken);

            return Result<string>.Success(session.GetPlainToken()!);
        }
    }
}