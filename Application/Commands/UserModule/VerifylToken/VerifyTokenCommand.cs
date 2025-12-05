using MediatR;
using SharedKernel.Common;
using Domain.AggregatesModel.VerificationAggregate;

namespace Application.Commands.UserModule.VerifyToken
{
    public record VerifyTokenCommand(
        string Token,
        TypeOfVerificationToken Type
    ) : IRequest<Result>;    
}