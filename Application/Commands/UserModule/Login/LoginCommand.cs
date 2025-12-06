using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.Login
{
    public readonly record struct LoginCommand(
        string Email,
        string Password,
        string DeviceInfo,
        string IpAddress
    ) : IRequest<Result<string>>;
}