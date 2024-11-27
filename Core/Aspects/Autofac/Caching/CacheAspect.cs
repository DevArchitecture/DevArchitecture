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
        private readonly string _cacheKey;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration">cache duration minutes time </param>
        /// <param name="cacheKey">Optional parameter. When you need of use a specific cache name. You can use of all methods.</param>
        public CacheAspect(int duration = 60, string cacheKey = null)
        {
            _duration = duration;
            _cacheKey = cacheKey;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Arguments[0]}.{invocation.Method.Name}");
            var arguments = invocation.Arguments;
            var key = string.IsNullOrEmpty(_cacheKey) ? $"{methodName}({BuildKey(arguments)})" : $"{_cacheKey}:{methodName}({BuildKey(arguments)})";
            var returnType = invocation.Method.ReturnType.GenericTypeArguments.FirstOrDefault();
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key, returnType);
                return;
            }

            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration, returnType);
        }


        string BuildKey(object[] args)
        {
            var sb = new StringBuilder();
            foreach (var arg in args)
            {
                var paramValues = arg.GetType().GetProperties()
                    .Select(p => p.GetValue(arg)?.ToString() ?? string.Empty);
                var enumerable = paramValues.ToList();
                if (enumerable.Any(w => w.Contains("Cancellation"))) continue;
                sb.Append(string.Join('_', enumerable));
            }

            return sb.ToString();
        }
    }
}