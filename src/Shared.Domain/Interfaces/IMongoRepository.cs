using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Shared.Domain.Interfaces
{
    public interface IMongoRepository<T> where T : class
    {
        IMongoQueryable<T> GetQuery(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(string id);
        Task DeleteRangeAsync(IEnumerable<string> ids);
        Task UpdateAsync(string id, T entity);
    }
}
