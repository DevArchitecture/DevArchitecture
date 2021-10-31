using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching.CacheManager;
using Microsoft.Extensions.Caching.Memory;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
	public class MemoryCacheProvider : ICacheProvider
	{
		public IMemoryCache Cache { get; set; }

		public MemoryCacheProvider()
		{
			Cache = new MemoryCache(new MemoryCacheOptions());
		}
		public Task<bool> DeleteAsync(string key)
		{
			Cache.Remove(key);
			return Task.FromResult(true);
		}
		public Task<bool> ExistsAsync(string key)
		{
			return Task.FromResult(Cache.TryGetValue(key, out _));
		}
		public Task<bool> SetAsync(string key, string serializedItem, TimeSpan? expiry)
		{
			Cache.Set(key, serializedItem);
			return Task.FromResult(true);
		}
		public Task<object> GetAsync(string key)
		{
			return Task.FromResult(Cache.Get<object>(key));
		}
		public async Task ClearAsync()
		{
			await Task.CompletedTask;
			Cache = new MemoryCache(new MemoryCacheOptions());
		}
		public void Dispose() => Cache.Dispose();

		public async Task RemoveByPatternAsync(string pattern)
		{
			await Task.Run(() =>
			{
				var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty(
						"EntriesCollection",
						BindingFlags.NonPublic | BindingFlags.Instance);
				var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(Cache) as dynamic;

				var cacheCollectionValues = new List<ICacheEntry>();

				foreach (var cacheItem in cacheEntriesCollection)
				{
					ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
					cacheCollectionValues.Add(cacheItemValue);
				}

				var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
				var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key)
						.ToList();
				foreach (var key in keysToRemove)
				{
					Cache.Remove(key);
				}
			});
		}


	}
}
