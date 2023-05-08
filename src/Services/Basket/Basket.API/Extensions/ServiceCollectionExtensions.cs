using Basket.API.Services;
using Basket.API.Services.Grpc;
using Basket.Domain.Interfaces;
using Basket.Infrastructure.DTOs;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Protos;

namespace Basket.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = RedisSettings.ConnectionString;
            });

            return services;
        }

        public static IServiceCollection AddBasketRepositories(this IServiceCollection services)
        {
            return services
                    .AddScoped<IBasketRepository, BasketRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                    .AddScoped<BasketService>()
                    .AddScoped<DiscountGrpcService>();
        }


        public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(_ =>
            {
                _.Address = new Uri(GrpcSettings.DiscountUrl);
            });

            return services;
        }

        public static IServiceCollection AddConfigSettings(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.GetSection("RedisSettings").Get<RedisSettings>(options => options.BindNonPublicProperties = true);
            configuration.GetSection("GrpcSettings").Get<GrpcSettings>(options => options.BindNonPublicProperties = true);

            return services;
        }
    }
}
