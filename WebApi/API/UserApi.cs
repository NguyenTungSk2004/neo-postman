using SharedKernel.Common;
using Application.Commands.UserModule.SignUpWithLocalAccount;
using Application.Commands.UserModule.UpdateProfile;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Contracts.Request;
using Application.Commands.UserModule.Login;

namespace WebApi.API
{
    public static class UserApi
    {
        public static RouteGroupBuilder MapUserApi(this IEndpointRouteBuilder app)
        {
            var api = app.MapGroup("api/users").WithTags("Users");

            api.MapGet("/registerLocalAccount", RegisterLocalAccount).WithOpenApi();
            api.MapPut("/updateProfile/{id:long}", UpdateProfile).WithOpenApi();
            return api;
        }
        private static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> RegisterLocalAccount(
            [FromBody] SignUpWithLocalAccountCommand command,
            [FromServices] IMediator mediator
        )
        {
            Result result = await mediator.Send(command);
            return result ? TypedResults.Ok() : TypedResults.BadRequest(result.Error);
        }

        private static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> UpdateProfile(
            [FromRoute] long id,
            [FromBody] UpdateProfileRequest request,
            [FromServices] IMediator mediator
        )
        {
            var command = new UpdateProfileCommand(id, request.Name, request.UrlAvatar);
            Result result = await mediator.Send(command);
            return result ? TypedResults.Ok() : TypedResults.BadRequest(result.Error);
        }
        
        private static async Task<Results<Ok<string>, BadRequest<string>, ProblemHttpResult>> Login(
            [FromBody] LoginRequest request,
            [FromServices] IMediator mediator
        )
        {
            string deviceInfo = $"{request.Device} - {request.Browser}";
            var command = new LoginCommand(
                request.Email,
                request.Password,
                deviceInfo,
                request.IpAddress
            );

            Result<string> result = await mediator.Send(command);
            return result ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.Error);
        }
    }
}
