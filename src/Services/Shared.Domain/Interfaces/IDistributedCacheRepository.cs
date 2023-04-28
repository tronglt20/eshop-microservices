namespace Shared.Domain.Interfaces
{
    public interface IDistributedCacheRepository
    {
        Task<string> GetAsync(string key);
        Task<T> GetAsync<T>(string key) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(IEnumerable<string> keys) where T : class;
        Task<IEnumerable<string>> GetListAsync(IEnumerable<string> keys);
        Task SetAsync(string key, string value);
        Task SetAsync<T>(string key, T value) where T : class;
        Task DeleteAsync(string key);
        Task DeleteRangeAsync(IEnumerable<string> keys);
        Task UpdateAsync(string key, string value);
        Task UpdateAsync<T>(string key, T value) where T : class;
    }
}
