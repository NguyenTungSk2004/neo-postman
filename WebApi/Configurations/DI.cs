using System.Reflection;

namespace WebApi.Configurations
{
    public static class DI
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Register your services here
            
            // MediatR - scan từ Assembly Application
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.Load("Application"));
            });
            return services;
        }
    }
}