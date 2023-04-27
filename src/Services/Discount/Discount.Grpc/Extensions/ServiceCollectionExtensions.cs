using Discount.Grpc.Data;
using Discount.Grpc.Interfaces;
using Discount.Grpc.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;
using Shared.Infrastructure;

namespace Discount.Grpc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscountDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DiscountContext>(options =>
            {
                options.UseNpgsql(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IUnitOfWorkBase<DiscountContext>, UnitOfWorkBase<DiscountContext>>();

            return services;
        }
    }
}
