using Basket.API.Services;
using Basket.Domain.Interfaces;
using Basket.Infrastructure.DTOs;
using Basket.Infrastructure.Repositories;

namespace Basket.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBasketCache(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.GetSection("RedisSettings").Get<RedisSettings>(options => options.BindNonPublicProperties = true);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = RedisSettings.ConnectionString;
            });

            return services;
        }

        public static IServiceCollection AddBasketRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<BasketService>();

            return services;
        }
    }
}
