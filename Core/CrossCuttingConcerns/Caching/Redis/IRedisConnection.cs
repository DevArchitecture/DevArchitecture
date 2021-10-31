using System;
using System.Net;
using StackExchange.Redis;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
	public interface IRedisConnection : IDisposable
	{
		RedisConfig Config { get; set; }
		IDatabase GetDatabase(int? db = null);
		IServer GetServer(EndPoint endPoint);
		EndPoint[] GetEndPoints();
		void FlushDatabase(int? db = null);
	}
}
