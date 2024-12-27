using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    /// <summary>
    /// CacheRemoveAspect
    /// </summary>
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private readonly ICacheManager _cacheManager;
        private readonly string _cacheKey;
        const string commandHandler = "CommandHandler";
        const string create = "Create";
        const string update = "Update";
        const string delete = "Delete";
        const string get = "Get";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern">CommandHandler, Get, Create, Update, Delete</param>
        /// <param name="cacheKey">If you used CacheKey when used CacheAspect. You can remove the cacheKey with this parameter.</param>
        public CacheRemoveAspect(string pattern = "", string cacheKey = null)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            _cacheKey = cacheKey;
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            if (string.IsNullOrEmpty(_pattern))
            {
                string targetTypeName = invocation.TargetType.Name;
                targetTypeName = targetTypeName.Replace(commandHandler, string.Empty);
                targetTypeName = targetTypeName.Replace(create, string.Empty);
                targetTypeName = targetTypeName.Replace(update, string.Empty);
                targetTypeName = targetTypeName.Replace(delete, string.Empty);
                _pattern = get + targetTypeName;
            }
            if (string.IsNullOrWhiteSpace(_cacheKey))
            {
                _cacheManager.RemoveByPattern(_pattern);
            }
            else
            {
                _cacheManager.Remove(_cacheKey);
            }

        }
    }
}
