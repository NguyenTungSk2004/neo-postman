using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.LoginWithToken
{
    public readonly record struct LoginWithTokenCommand(
        string RefreshToken,
        string DeviceInfo,
        string IpAddress
    ) : IRequest<Result>;
}