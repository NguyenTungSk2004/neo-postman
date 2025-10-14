using Application.Commands.UserModule.SignUpWithLocalAccount;
using Application.Commands.UserModule.UpdateProfile;
using Application.Common.Types;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.API
{
    public static class UserApi
    {
        public static RouteGroupBuilder MapUserApi(this IEndpointRouteBuilder app)
        {
            var api = app.MapGroup("api/users").WithTags("Users");

            api.MapGet("/registerLocalAccount", RegisterLocalAccount).WithOpenApi();
            api.MapPut("/updateProfile", UpdateProfile).WithOpenApi();
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
            [FromBody] UpdateProfileCommand command,
            [FromServices] IMediator mediator
        )
        {
            Result result = await mediator.Send(command);
            return result ? TypedResults.Ok() : TypedResults.BadRequest(result.Error);
        }
        private record UpdateProfileRequest(
            string Name,
            string? UrlAvatar
        );
    }
}
