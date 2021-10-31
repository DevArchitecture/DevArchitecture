using System;
using System.Net;
using StackExchange.Redis;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
	public class RedisConnection : IRedisConnection
	{
		#region Fields
		public RedisConfig Config { get; set; }
		private Lazy<IConnectionMultiplexer> _connection;
		#endregion
		#region Ctor

		public RedisConnection(RedisConfig config)
		{
			Config = config;
		}

		#endregion

		#region Utilities

		protected IConnectionMultiplexer GetConnection()
		{
			if (_connection == null || !_connection.IsValueCreated)
			{
				_connection = new Lazy<IConnectionMultiplexer>(() =>
						ConnectionMultiplexer.Connect(Config.Connection));
			}

			return _connection.Value;
		}

		#endregion

		#region Methods

		public IDatabase GetDatabase(int? db = null)
		{
			return GetConnection().GetDatabase(db ?? -1);
		}
		public IServer GetServer(EndPoint endPoint)
		{
			return GetConnection().GetServer(endPoint);
		}


		public EndPoint[] GetEndPoints()
		{
			return GetConnection().GetEndPoints();
		}

		public void FlushDatabase(int? db = null)
		{
			var endPoints = GetEndPoints();

			foreach (var endPoint in endPoints)
			{
				GetServer(endPoint).FlushDatabase(db ?? -1);
			}
		}

		public void Dispose()
		{
			if (_connection.IsValueCreated)
				_connection?.Value.Dispose();

		}

		#endregion
	}
}
