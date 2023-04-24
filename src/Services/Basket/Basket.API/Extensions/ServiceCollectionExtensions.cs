using Basket.Infrastructure.DTOs;

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
    }
}
