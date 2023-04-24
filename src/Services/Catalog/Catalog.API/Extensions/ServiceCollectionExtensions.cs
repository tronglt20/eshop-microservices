using Catalog.API.Data;
using Catalog.API.Interfaces;
using Catalog.API.Repositories;
using Catalog.API.Services;
using MongoDB.Driver;

namespace Catalog.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogMongoDatabaseContext(this IServiceCollection services
            , IConfiguration configuration)
        {
            var dbconnection = configuration.GetSection("DatabaseSettings").Get<CatalogDbConnection>();
            services.AddScoped(_ =>
            {
                var clientSettings = MongoClientSettings.FromConnectionString(dbconnection.ConnectionString);
                var client = new MongoClient(clientSettings);

                var database = client.GetDatabase(dbconnection.DatabaseName);
                return new CatalogMongoDatabaseOptions
                {
                    Database = database
                };
            });

            return services;
        }

        public static IServiceCollection AddCatalogRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<CatalogService>();

            return services;
        }
    }
}
