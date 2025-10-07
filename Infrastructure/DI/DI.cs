using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Domain.SeedWork;
using Application.Common.Interfaces;

namespace Infrastructure.DI
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=.;Database=neo-postman;Trusted_Connection=True;TrustServerCertificate=True;"));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
