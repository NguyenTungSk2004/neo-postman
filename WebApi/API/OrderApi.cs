namespace WebApi.API
{
    public static class OrderApi
    {
        public static RouteGroupBuilder MapOrderApi(this IEndpointRouteBuilder app)
        {
            var api = app.MapGroup("api/orders").WithTags("Orders");

            api.MapGet("/", () => "Order API is working!")
                .WithOpenApi();

            api.MapGet("/order", () => "Order API is working!")
                .WithOpenApi();

            api.MapGet("/order/{id}", (int id) => $"Order API is working for order {id}")
                .WithOpenApi();

            return api;
        }
    }
}
