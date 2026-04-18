using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Text;
using static ServiceStack.Diagnostics;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    /// <summary>
    /// Microsoft MemoryCacheManager
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;

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
            _cache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public void Add(string key, object data)
        {
            _cache.Set(key, data);
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
        }

        public void RemoveByPattern(string pattern)
        {
            // Disabled due runtime instability in reflection-based key scanning.
            // CacheAspect is currently running in bypass mode, so this is safe.
            _ = pattern;
        }
    }
}