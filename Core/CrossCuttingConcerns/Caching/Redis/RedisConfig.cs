using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
	public class RedisConfig
	{
		public string Connection { get; set; }
		public int Database { get; set; }
	}
}
