using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.UpdateProfile
{
    public record UpdateProfileCommand(
        long Id,
        string Name,
        string? UrlAvatar
    ): IRequest<Result>;
}