using MediatR;

namespace Application.Commands.UserModule.SignUpWithLocalAccount
{
    public record SignUpWithLocalAccountCommand(
        string Name,
        string Email,
        string Password
    ): IRequest<bool>;
}