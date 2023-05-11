using Microsoft.EntityFrameworkCore;
using Ordering.API.Services;
using Ordering.Domain.Interfaces;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Repositories;
using Shared.Domain.Interfaces;
using Shared.Infrastructure;

namespace Ordering.API.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddOrderDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            });

            // Database Migrations
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var servicesMigration = scope.ServiceProvider;
                var context = servicesMigration.GetRequiredService<OrderContext>();
                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWorkBase<OrderContext>, UnitOfWorkBase<OrderContext>>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<OrderingService>();
            return services;
        }
    }
}
