using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.SignUpWithLocalAccount
{
    public readonly record struct SignUpWithLocalAccountCommand(
        string Name,
        string Email,
        string Password
    ) : IRequest<Result>;
}