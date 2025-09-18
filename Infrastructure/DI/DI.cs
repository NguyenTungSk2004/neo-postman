using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using SharedKernel.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DI
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=app.db")); // hoặc UseNpgsql, UseSqlite...

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }
}
