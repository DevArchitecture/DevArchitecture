using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.CacheManager;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
	/// <summary>
	/// CacheAspect
	/// </summary>
	public class CacheAspect : MethodInterception
	{
		private readonly int _duration;
		private readonly ICacheManager _cacheManager;
		private Type type;

		public CacheAspect(int duration = 60,Type type=null)
		{
			this.type = type;
			_duration = duration;
			_cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
		}

		public override void Intercept(IInvocation invocation)
		{
			
			var methodName = string.Format($"{invocation.Arguments[0]}.{invocation.Method.Name}");
			var arguments = invocation.Arguments;
			var key = $"{methodName}({BuildKey(arguments)})";
			if (_cacheManager.IsSetAsync(key).Result)
			{
				var getCache = _cacheManager.GetAsync<dynamic>(key);
	
				invocation.ReturnValue = getCache;
				return;
			}

			invocation.Proceed();
			_cacheManager.SetAsync(key, invocation.ReturnValue, System.TimeSpan.FromMinutes(_duration)).Wait();
		}


		string BuildKey(object[] args)
		{
			var sb = new StringBuilder();
			foreach (var arg in args)
			{
				var paramValues = arg.GetType().GetProperties()
						.Select(p => p.GetValue(arg)?.ToString() ?? string.Empty);
				sb.Append(string.Join('_', paramValues));
			}

			return sb.ToString();
		}
	}
}