using Domain.AggregatesModel.UserAggregate;
using Domain.SeedWork;
using MediatR;

namespace Application.Commands.UserModule.UpdateProfile
{
    public class UpdateProfileHandler: IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        public UpdateProfileHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id) ?? throw new Exception("User not found");
            user.Update(request.Name, request.UrlAvatar);
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}