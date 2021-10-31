using System;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.CacheManager
{
    public interface ICacheManager : IDisposable
    {
        Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, TimeSpan? expiry = null);
        Task<T> GetAsync<T>(string key);
        Task<bool> SetAsync(string key, object data, TimeSpan? expiry = null);
        Task<bool> IsSetAsync(string key);
        Task<bool> RemoveAsync(string key);
        Task RemoveByPatternAsync(string pattern);
        Task ClearAsync();
    }
}