using Catalog.API.Data;
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
    }
}
