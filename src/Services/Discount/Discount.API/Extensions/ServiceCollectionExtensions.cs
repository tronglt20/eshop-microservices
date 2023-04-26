using Discount.API.Services;
using Discount.Domain.Interfaces;
using Discount.Infrastructure;
using Discount.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;
using Shared.Infrastructure;

namespace Discount.API.Extensions
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

        public static IServiceCollection AddDiscountRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IUnitOfWorkBase<DiscountContext>, UnitOfWorkBase<DiscountContext>>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<DiscountService>();

            return services;
        }
    }
}
