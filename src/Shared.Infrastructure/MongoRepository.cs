using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.DTOs;
using System.Linq.Expressions;

namespace Shared.Infrastructure
{
    public class MongoRepository<T> : IMongoRepository<T> where T : class
    {
        private readonly UserInfo UserInfo;
        private readonly IMongoDatabase Database;
        private readonly IServiceProvider ServiceProvider;
        protected IMongoCollection<T> Collection => Database.GetCollection<T>(typeof(T).Name);

        public MongoRepository(IMongoDatabase database, IServiceProvider serviceProvider)
        {
            Database = database;
            ServiceProvider = serviceProvider;
            UserInfo = serviceProvider.GetService<UserInfo>();
        }

        public IMongoQueryable<T> GetQuery(Expression<Func<T, bool>> expression)
        {
            return Collection.AsQueryable().Where(expression);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Collection.AsQueryable().ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            var entities = await Collection.FindAsync(expression);
            return await entities.AnyAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            var entities = await Collection.FindAsync(expression);
            return await entities.FirstOrDefaultAsync();
        }

        public async Task InsertAsync(T entity)
        {
            OnEntitiesAdded(entity);
            await Collection.InsertOneAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                OnEntitiesAdded(entities.ToArray());
                await Collection.InsertManyAsync(entities);
            }
        }

        public async Task DeleteAsync(string id)
        {
            await Collection.DeleteOneAsync(FilterId(id));
        }

        public async Task DeleteRangeAsync(IEnumerable<string> ids)
        {
            if (ids.Any())
            {
                foreach (var id in ids)
                {
                    await DeleteAsync(id);
                }
            }
        }

        public async Task UpdateAsync(string id, T entity)
        {
            await Collection.ReplaceOneAsync(FilterId(id), entity);
        }

        #region Handle SaveChange Entities
        private void OnEntitiesAdded(params T[] entities)
        {
            // Custom savechange here
        }
        #endregion

        #region Helper
        private static FilterDefinition<T> FilterId(string key)
        {
            return Builders<T>.Filter.Eq("Id", key);
        }
        #endregion
    }
}
