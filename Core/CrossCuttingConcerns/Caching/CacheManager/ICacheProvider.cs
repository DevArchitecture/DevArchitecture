using System;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.CacheManager
{
    public interface ICacheProvider : IDisposable
    {
        Task RemoveByPatternAsync(string pattern);
        Task ClearAsync();
        Task<bool> DeleteAsync(string key);
        Task<bool> ExistsAsync(string key);
        Task<bool> SetAsync(string key, string serializedItem, TimeSpan? expiry);
        Task<object> GetAsync(string key);
    }
}
