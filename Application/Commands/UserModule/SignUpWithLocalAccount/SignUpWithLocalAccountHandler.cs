using System.Transactions;
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
        private readonly IPasswordHasher _passwordHasher;
        public SignUpWithLocalAccountHandler(
            IRepository<User> userRepository,
            IRepository<UserVerificationToken> userVerificationTokenRepository,
            IPasswordHasher passwordHasher
        )
        {
            _userRepository = userRepository;
            _userVerificationTokenRepository = userVerificationTokenRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result> Handle(SignUpWithLocalAccountCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var emailSpec = new UserByEmailSpecification(request.Email);
                if (await _userRepository.AnyAsync(emailSpec))
                    return Result.Failure("Email is already registered");
                    
                string hash = _passwordHasher.HashPassword(request.Password);
                var user = User.CreateLocalAccount(request.Name, request.Email, hash);
                var token = UserVerificationToken.GenerateToken(user.Id, TypeOfVerificationToken.EmailVerification, TimeSpan.FromHours(1));
                
                await _userRepository.AddAsync(user, cancellationToken);
                await _userVerificationTokenRepository.AddAsync(token, cancellationToken);
                
                transaction.Complete();
                return Result.Success();
            }
        }
    }
}