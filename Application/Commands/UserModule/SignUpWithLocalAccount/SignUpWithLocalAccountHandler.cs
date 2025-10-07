using Application.Common.Interfaces;
using Domain.AggregatesModel.UserAggregate;
using Domain.AggregatesModel.UserAggregate.Specifications;
using Domain.SeedWork;
using MediatR;

namespace Application.Commands.UserModule.SignUpWithLocalAccount
{
    public class SignUpWithLocalAccountHandler : IRequestHandler<SignUpWithLocalAccountCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public SignUpWithLocalAccountHandler(IRepository<User> userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> Handle(SignUpWithLocalAccountCommand request, CancellationToken cancellationToken)
        {
            var emailSpec = new UserByEmailSpecification(request.Email);
            if (await _userRepository.AnyAsync(emailSpec))
            {
                throw new Exception("Email already in use");
            }
            string hash = _passwordHasher.HashPassword(request.Password);
            var user = User.CreateLocalAccount(request.Name, request.Email, hash);
            await _userRepository.AddAsync(user);
            return true;
        }
    }
}