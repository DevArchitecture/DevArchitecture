using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
	public static class PackagesExtensions
	{
		public static IServiceCollection AddCacheRedis(this IServiceCollection services, RedisConfig config)
		{
			var provider = new RedisCacheProvider(new RedisConnection(config));

			services.AddSingleton<ICacheManager>(new CacheManager(provider));
			return services;
		}
	}
}