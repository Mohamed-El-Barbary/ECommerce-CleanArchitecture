using ECommerce.Domain.Repositories;
using ECommerce.Infrastructure.Persistence.Data.DbContexts;
using ECommerce.Infrastructure.Persistence.Seeding;
using ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"))
                       .EnableSensitiveDataLogging();
            });

            services.AddScoped<DatabaseSeeder>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
