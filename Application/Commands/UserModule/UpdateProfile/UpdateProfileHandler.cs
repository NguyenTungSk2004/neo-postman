using Domain.AggregatesModel.UserAggregate;
using Domain.SeedWork;
using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.UpdateProfile
{
    public class UpdateProfileHandler: IRequestHandler<UpdateProfileCommand, Result>
    {
        private readonly IRepository<User> _userRepository;
        public UpdateProfileHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user is null)
                return Result.Failure("User not found");
            user.Update(request.Name, request.UrlAvatar);
            await _userRepository.UpdateAsync(user);
            return Result.Success();
        }
    }
}