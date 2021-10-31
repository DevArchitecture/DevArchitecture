using Core.CrossCuttingConcerns.Caching.CacheManager;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
	public static class PackagesExtensions
	{
		public static IServiceCollection AddCacheMemory(this IServiceCollection services)
		{
			services.AddSingleton<ICacheManager>(new CacheManager(new MemoryCacheProvider()));
			return services;
		}
	}
}