using eshop.Client.Services;

namespace eshop.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                    .AddScoped<RestClientService>()
                    .AddScoped<CatalogService>()
                    .AddScoped<BasketService>()
                    .AddScoped<OrderService>();

            return services;
        }
    }
}
