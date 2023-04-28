using Microsoft.Extensions.Caching.Distributed;
using Shared.Domain.Interfaces;
using System.Text.Json;

namespace Shared.Infrastructure
{
    public class DistributedCacheRepository : IDistributedCacheRepository
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task DeleteAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task DeleteRangeAsync(IEnumerable<string> keys)
        {
            if (keys.Any())
            {
                foreach (var key in keys)
                {
                    await DeleteAsync(key);
                }
            }
        }

        public async Task<string> GetAsync(string key)
        {
            return await _distributedCache.GetStringAsync(key);
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var jsonString = await _distributedCache.GetStringAsync(key);
            if(string.IsNullOrEmpty(jsonString))
                return null;

            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(IEnumerable<string> keys) where T : class
        {
            var result = new List<T>();

            foreach (var key in keys)
            {
                var value = await GetAsync<T>(key);
                if (value == null)
                    continue;
                result.Add(value);
            }

            return result;
        }

        public async Task<IEnumerable<string>> GetListAsync(IEnumerable<string> keys)
        {
            var result = new List<string>();

            foreach (var key in keys)
            {
                var value = await GetAsync(key);
                if(value == null)
                    continue;
                result.Add(value);
            }

            return result;
        }

        public async Task SetAsync(string key, string value)
        {
            await _distributedCache.SetStringAsync(key, value);
        }

        public async Task SetAsync<T>(string key, T value) where T : class
        {
            var stringValue = JsonSerializer.Serialize(value);
            await _distributedCache.SetStringAsync(key, stringValue);
        }

        public async Task UpdateAsync(string key, string value)
        {
            await SetAsync(key, value);
        }

        public async Task UpdateAsync<T>(string key, T value) where T : class
        {
            await SetAsync(key, value);
        }
    }
}
