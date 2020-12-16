using ServiceStack.Redis;
using System;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    /// <summary>
    /// RedisCacheManager
    /// </summary>
    public class RedisCacheManager : ICacheManager
    {
        private readonly RedisEndpoint _redisEndpoint;

        private void RedisInvoker(Action<RedisClient> redisAction)
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                redisAction.Invoke(client);
            }
        }

        public RedisCacheManager()
        {
            _redisEndpoint = new RedisEndpoint("localhost", 6379);
        }

        public T Get<T>(string key)
        {
            var result = default(T);
            RedisInvoker(x => { result = x.Get<T>(key); });
            return result;
        }

        public object Get(string key)
        {
            var result = default(object);
            RedisInvoker(x => { result = x.Get<object>(key); });
            return result;
        }

        public void Add(string key, object data, int duration)
        {
            RedisInvoker(x => x.Add(key, data, TimeSpan.FromMinutes(duration)));
        }

        public bool IsAdd(string key)
        {
            var isAdded = false;
            RedisInvoker(x => isAdded = x.ContainsKey(key));
            return isAdded;
        }

        public void Remove(string key)
        {
            RedisInvoker(x => x.Remove(key));
        }

        public void RemoveByPattern(string pattern)
        {
            RedisInvoker(x => x.RemoveByPattern(pattern));
        }

        public void Clear()
        {
            RedisInvoker(x => x.FlushAll());
        }
    }
}
