using Application.Common.Interfaces;
using Domain.AggregatesModel.UserAggregate;
using Domain.AggregatesModel.UserAggregate.Specifications;
using Domain.AggregatesModel.VerificationAggregate;
using Domain.SeedWork;
using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.SignUpWithLocalAccount
{
    public class SignUpWithLocalAccountHandler : IRequestHandler<SignUpWithLocalAccountCommand, Result>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserVerificationToken> _userVerificationTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        public SignUpWithLocalAccountHandler(
            IRepository<User> userRepository,
            IRepository<UserVerificationToken> userVerificationTokenRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher
        )
        {
            _userRepository = userRepository;
            _userVerificationTokenRepository = userVerificationTokenRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result> Handle(SignUpWithLocalAccountCommand request, CancellationToken cancellationToken)
        {
            var emailSpec = new UserByEmailSpecification(request.Email);
            if (await _userRepository.AnyAsync(emailSpec))
                return Result.Failure("Email is already registered");
                
            string hash = _passwordHasher.HashPassword(request.Password);
            var user = User.CreateLocalAccount(request.Name, request.Email, hash);
            var token = UserVerificationToken.GenerateToken(user.Id, TypeOfVerificationToken.EmailVerification, TimeSpan.FromHours(1));
            
            _userRepository.Add(user);
            _userVerificationTokenRepository.Add(token);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}