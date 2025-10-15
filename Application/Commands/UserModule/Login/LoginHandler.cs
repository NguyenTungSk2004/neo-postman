using Application.Common.Interfaces;
using Domain.AggregatesModel.SessionAggregate;
using Domain.AggregatesModel.SessionAggregate.Specifications;
using Domain.AggregatesModel.UserAggregate.Entities;
using Domain.AggregatesModel.UserAggregate.Enums;
using Domain.AggregatesModel.UserAggregate.Specifications;
using Domain.SeedWork;
using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.Login
{
    public class LoginHandler: IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserSession> _userSessionRepository;
        private readonly IPasswordHasher _passwordHasher;
        public LoginHandler(
            IRepository<User> userRepository,
            IRepository<UserSession> userSessionRepository,
            IPasswordHasher passwordHasher
        )
        {
            _userRepository = userRepository;
            _userSessionRepository = userSessionRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var spec = new UserByEmailSpecification(request.Email);
            var user = await _userRepository.FirstOrDefaultAsync(spec);
            if (user is null)
                return Result<string>.Failure("Email is not registered. Please sign up first.");

            if (user.EmailVerifiedAt is null)
            {
                var existingToken = user.CurrentVerificationToken.FirstOrDefault(x => x.IsValid());
                if (existingToken is null)
                {
                    user.AddToken(TypeOfVerificationToken.EmailVerification);
                    await _userRepository.SaveChangesAsync(cancellationToken);
                }
                return Result<string>.Failure("Email is not verified. Please verify your email before logging in.");
            }
            
            if (user.IsDisabled)
                return Result<string>.Failure("User account is disabled. Please contact admin.");

            var localAccount = user.UserAuthProviders.FirstOrDefault(x => x.Provider == AuthProvider.Local);
            if (localAccount is null)
                return Result<string>.Failure("Local account is not set up for this user. Please use external login.");

            if (!_passwordHasher.VerifyPassword(request.Password, localAccount.PasswordHash!))
                return Result<string>.Failure("Password is incorrect. Please try again.");

            var session = UserSession.CreateNewSession(user.Id, request.DeviceInfo, request.IpAddress);
            await _userSessionRepository.AddAsync(session, cancellationToken);

            return Result<string>.Success(session.GetPlainToken()!);
        }
    }
}