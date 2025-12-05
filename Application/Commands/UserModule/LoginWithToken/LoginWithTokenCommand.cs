using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.LoginWithToken
{
    public record LoginWithTokenCommand(
        string RefreshToken,
        string DeviceInfo,
        string IpAddress
    ) : IRequest<Result>;    
}