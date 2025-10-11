
using Application.Commands.UserModule.SignUpWithLocalAccount;
using MediatR;

namespace BlazorWebApp.Services;
public class UserService
{
    private readonly IMediator _mediator;

    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RegisterLocalAccount(string name, string email, string password)
    {
        var command = new SignUpWithLocalAccountCommand(name, email, password);
        await _mediator.Send(command);
    }
}