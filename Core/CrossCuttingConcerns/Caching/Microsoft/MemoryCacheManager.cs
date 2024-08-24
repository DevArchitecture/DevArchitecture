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
            var json = JsonSerializer.SerializeToString(data.Result, type);
            Add(key, json, duration);
        }

        public void Add(string key, dynamic data, Type type)
        {
            var json = JsonSerializer.SerializeToString(data.Result, type);
            Add(key, json);
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
            var json = Get<string>(key);
            var result = JsonSerializer.DeserializeFromString(json, type);

            return typeof(Task)
                .GetMethod(nameof(Task.FromResult))
                .MakeGenericMethod(type)
                .Invoke(this, new object[] { result });
        }

        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public bool IsConnected()
        {
            return true;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var coherentState = typeof(MemoryCache).GetField("_coherentState", BindingFlags.NonPublic | BindingFlags.Instance);

            var coherentStateValue = coherentState.GetValue(_cache);
            var cacheEntriesCollectionDefinition = coherentStateValue.GetType().GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
      

            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(coherentStateValue) as ICollection;

            var cacheCollectionValues = new List<string>();

            if (cacheEntriesCollection != null)
            {
                foreach (var item in cacheEntriesCollection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");

                    var val = methodInfo.GetValue(item);

                    cacheCollectionValues.Add(val.ToString());
                }
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d)).Select(d => d)
                .ToList();
            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
        }
    }
}