using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Text;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

/// <summary>
/// Microsoft MemoryCacheManager
/// </summary>
public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache _cache;
    private static readonly HashSet<string> _cacheKeys = new HashSet<string>();

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
        _cacheKeys.Add(key);
    }

    public void Add(string key, object data)
    {
        _cache.Set(key, data);
        _cacheKeys.Add(key);
    }

    public void Add(string key, dynamic data, int duration, Type type)
    {
        var json = JsonSerializer.SerializeToString(data.Result, type);
        Add(key, json, duration);
        _cacheKeys.Add(key);
    }

    public void Add(string key, dynamic data, Type type)
    {
        var json = JsonSerializer.SerializeToString(data.Result, type);
        Add(key, json);
        _cacheKeys.Add(key);
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


    public void Remove(string key)
    {
        _cache.Remove(key);
        _cacheKeys.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
        var keysToRemove = _cacheKeys
        .Where(k => Regex.IsMatch(k, pattern, RegexOptions.IgnoreCase))
        .ToArray();

        foreach (var key in keysToRemove)
        {
            _cache.Remove(key);
            _cacheKeys.Remove(key);
        }
    }
}
