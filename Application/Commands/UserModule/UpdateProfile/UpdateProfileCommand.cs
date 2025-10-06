using MediatR;

namespace Application.Commands.UserModule.UpdateProfile
{
    public record UpdateProfileCommand(
        long Id,
        string Name,
        string? UrlAvatar
    ): IRequest<bool>;
}