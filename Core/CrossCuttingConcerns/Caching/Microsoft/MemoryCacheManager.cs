using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Text;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    /// <summary>
    /// Microsoft MemoryCacheManager
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, byte> _keys = new ConcurrentDictionary<string, byte>();

        public MemoryCacheManager()
            : this(ServiceTool.ServiceProvider.GetService<IMemoryCache>())
        {
        }

        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Add(string key, object data, int duration)
        {
            Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public void Add(string key, object data)
        {
            Set(key, data, null);
        }

        public void Add(string key, dynamic data, int duration, Type type)
        {
            var value = data?.Result ?? data;
            Add(key, value, duration);
        }

        public void Add(string key, dynamic data, Type type)
        {
            var value = data?.Result ?? data;
            Add(key, value);
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public object Get(string key, Type type)
        {
            var cached = Get(key);
            if (cached == null)
            {
                return null;
            }

            object result = cached;
            if (cached is string json && type != null)
            {
                result = JsonSerializer.DeserializeFromString(json, type);
            }

            if (type == null)
            {
                return result;
            }

            return typeof(Task)
                .GetMethod(nameof(Task.FromResult))
                .MakeGenericMethod(type)
                .Invoke(this, new object[] { result });
        }

        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _keys.TryRemove(key, out _);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (var key in _keys.Keys.Where(k => regex.IsMatch(k)).ToList())
            {
                Remove(key);
            }
        }

        private void Set(string key, object data, TimeSpan? expiration)
        {
            var options = new MemoryCacheEntryOptions()
                .RegisterPostEvictionCallback((evictedKey, _, _, _) => _keys.TryRemove((string)evictedKey, out _));

            if (expiration.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = expiration;
            }

            _keys[key] = 0;
            _cache.Set(key, data, options);
        }
    }
}
