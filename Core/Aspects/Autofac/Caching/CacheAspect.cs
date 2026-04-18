using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
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
        
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            // Temporary safe mode: bypass cache interception to prevent
            // stale/invalid cache materialization from breaking CRUD list endpoints.
            invocation.Proceed();
        }


        string BuildKey(object[] args)
        {
            var sb = new StringBuilder();
            foreach (var arg in args)
            {
                var paramValues = arg.GetType().GetProperties()
                    .Select(p => p.GetValue(arg)?.ToString() ?? string.Empty);
                var enumerable = paramValues.ToList();
                if(enumerable.Any(w => w.Contains("Cancellation"))) continue;
                sb.Append(string.Join('_', enumerable));
            }

            return sb.ToString();
        }
    }
}