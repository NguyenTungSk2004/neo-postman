using Application.Common.Interfaces;
using Domain.AggregatesModel.UserAggregate;
using Domain.AggregatesModel.UserAggregate.Specifications;
using Domain.AggregatesModel.VerificationAggregate;
using Domain.SeedWork;
using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.SignUpWithLocalAccount
{
    public class SignUpWithLocalAccountHandler(
        IRepository<User> userRepository,
        IRepository<UserVerificationToken> userVerificationTokenRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher
    ) : IRequestHandler<SignUpWithLocalAccountCommand, Result>
    {
        public async Task<Result> Handle(SignUpWithLocalAccountCommand request, CancellationToken cancellationToken)
        {
            var emailSpec = new UserByEmailSpecification(request.Email);
            if (await userRepository.AnyAsync(emailSpec))
                return Result.Failure("Email is already registered");

            string hash = passwordHasher.HashPassword(request.Password);
            var user = User.CreateLocalAccount(request.Name, request.Email, hash);
            var token = UserVerificationToken.GenerateToken(user.Id, TypeOfVerificationToken.EmailVerification, TimeSpan.FromHours(1));

            userRepository.Add(user);
            userVerificationTokenRepository.Add(token);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}