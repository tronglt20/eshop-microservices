using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogMongoDatabaseOptions
    {
        public IMongoDatabase Database { get; set; }
    }
}
