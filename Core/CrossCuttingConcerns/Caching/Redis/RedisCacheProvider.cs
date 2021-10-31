using System;
using System.Linq;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching.CacheManager;
using StackExchange.Redis;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
	public class RedisCacheProvider : ICacheProvider
	{
		private readonly IRedisConnection _connection;
		private readonly IDatabase _db;
		public RedisCacheProvider(IRedisConnection connection)
		{
			if (connection.Config == null)
				throw new SystemException("RedisConfigCanNotBeNull");

			_connection = connection;

			_db = _connection.GetDatabase(connection.Config.Database);
		}


		public async Task RemoveByPatternAsync(string pattern)
		{
			foreach (var endPoint in _connection.GetEndPoints())
			{
				var server = _connection.GetServer(endPoint);
				var keys = server.Keys(_db.Database, $"*{pattern}*");
				await _db.KeyDeleteAsync(keys.ToArray());
			}
		}

		public async Task ClearAsync()
		{
			foreach (var endPoint in _connection.GetEndPoints())
			{
				var server = _connection.GetServer(endPoint);
				var keys = server.Keys(_db.Database);
				await _db.KeyDeleteAsync(keys.ToArray());
			}
		}

		public async Task<bool> DeleteAsync(string key)
		{
			return await _db.KeyDeleteAsync(key);
		}

		public async Task<bool> ExistsAsync(string key)
		{
			return await _db.KeyExistsAsync(key);
		}

		public async Task<bool> SetAsync(string key, string serializedItem, TimeSpan? expiry)
		{
			return await _db.StringSetAsync(key, serializedItem, expiry);
		}

		public async Task<object> GetAsync(string key)
		{
			return await _db.StringGetAsync(key);
		}
		public void Dispose() => _connection?.Dispose();

	}
}
