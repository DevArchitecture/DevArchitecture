using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ServiceStack.Text;
using StackExchange.Redis;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    /// <summary>
    /// RedisCacheManager
    /// </summary>
    public class RedisCacheManager : ICacheManager
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisCacheManager(IConfiguration configuration)
        {
            var cacheConfig = configuration.GetSection(nameof(CacheOptions)).Get<CacheOptions>();
            var configurationOptions = ConfigurationOptions.Parse($"{cacheConfig.Host}:{cacheConfig.Port}");
            if (!string.IsNullOrEmpty(cacheConfig.Password))
            {
                configurationOptions.Password = cacheConfig.Password;
            }

            configurationOptions.DefaultDatabase = cacheConfig.Database;
            _redis = ConnectionMultiplexer.Connect(configurationOptions);
        }

        public T Get<T>(string key)
        {
            var result = default(T);
            RedisInvoker(cache =>
            {
                var data = cache.StringGet(key);
                if (data.HasValue)
                {
                    result = JsonSerializer.DeserializeFromString<T>(data);
                }
            });
            return result;
        }

        public object Get(string key)
        {
            var result = default(object);
            RedisInvoker(cache =>
            {
                var data = cache.StringGet(key);
                if (data.HasValue)
                {
                    result = JsonSerializer.DeserializeFromString<object>(data);
                }
            });
            return result;
        }

        public object Get(string key, Type type)
        {
            var data = Get<string>(key);
            var result = data != null ? JsonSerializer.DeserializeFromString(data, type) : null;
            return typeof(Task)
                .GetMethod(nameof(Task.FromResult))!
                .MakeGenericMethod(type)
                .Invoke(this, new object[] { result });
        }

        public void Add(string key, object data, int duration)
        {
            RedisInvoker(cache => cache.StringSet(key, JsonSerializer.SerializeToString(data), TimeSpan.FromMinutes(duration)));
        }

        public void Add(string key, object data)
        {
            RedisInvoker(cache => cache.StringSet(key, JsonSerializer.SerializeToString(data)));
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

        public bool IsAdd(string key)
        {
            var isAdded = false;
            RedisInvoker(cache => isAdded = cache.KeyExists(key));
            return isAdded;
        }

        public void Remove(string key)
        {
            RedisInvoker(cache => cache.KeyDelete(key));
        }

        public async void RemoveByPattern(string pattern)
        {
            await _redis.KeyDeleteByPatternAsync(pattern: pattern, 0);
        }

        public void Clear()
        {
            var redisServer = _redis.GetServer(_redis.Configuration);
            var redisDatabase = _redis.GetDatabase();
            redisServer.FlushDatabase(redisDatabase.Database);
        }

        private void RedisInvoker(Action<IDatabase> redisAction)
        {
            var redisDatabase = _redis.GetDatabase();
            redisAction.Invoke(redisDatabase);
        }
    }

    public static class RedisExtensions
    {
        public static async ValueTask KeyDeleteByPatternAsync(
            this IConnectionMultiplexer multiplexer,
            string pattern = null,
            int database = -1)
        {
            // there may be multiple endpoints behind a multiplexer
            var endpoints = multiplexer.GetEndPoints();
            var db = multiplexer.GetDatabase(database);
            RedisKey[] keys;

            foreach (var ep in endpoints)
            {
                var server = multiplexer.GetServer(ep);
                if (!server.IsConnected || server.IsReplica) continue;
                keys = server.Keys(database, $"*{pattern}*").ToArray();
                await FlushBatch().ConfigureAwait(false);
            }

            return;

            Task FlushBatch()
            {
                return keys.Length == 0 ? Task.CompletedTask : db.KeyDeleteAsync(keys);
            }
        }
    }
}