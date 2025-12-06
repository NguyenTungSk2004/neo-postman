using Domain.AggregatesModel.VerificationAggregate;
using MediatR;
using SharedKernel.Common;

namespace Application.Commands.UserModule.VerifyToken
{
    public readonly record struct VerifyTokenCommand(
        string Token,
        TypeOfVerificationToken Type
    ) : IRequest<Result>;
}