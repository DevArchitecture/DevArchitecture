using System;
using System.Dynamic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Caching.CacheManager
{
    public class CacheManager : ICacheManager
    {
        private readonly ICacheProvider _provider;

        public CacheManager(ICacheProvider provider)
        {
            _provider = provider;
        }
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, TimeSpan? expiry = null)
        {
            if (await IsSetAsync(key))
            {
                var serializedItem = await _provider.GetAsync(key);
                if (serializedItem == null)
                    return default;

                var item = JsonConvert.DeserializeObject<T>((string)serializedItem);

                return item ?? default;
            }

            if (acquire == null)
                return default;

            var result = await acquire();
            await SetAsync(key, result, expiry);
            return result;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await GetAsync<T>(key, null);
        }
        public async Task<bool> SetAsync(string key, object data, TimeSpan? expiry = null)
        {
            var serializedItem = JsonConvert.SerializeObject(data);
            return await _provider.SetAsync(key, serializedItem, expiry);
        }
        public async Task<bool> IsSetAsync(string key)
        {
            return await _provider.ExistsAsync(key);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _provider.DeleteAsync(key);
        }
        public async Task RemoveByPatternAsync(string pattern)
        {
            await _provider.RemoveByPatternAsync(pattern);
        }
        public async Task ClearAsync()
        {
            await _provider.ClearAsync();
        }
        public void Dispose()
        {
            _provider?.Dispose();
        }
    }
}
