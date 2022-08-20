using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Settings;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching;

/// <summary>
/// CacheRemoveAspect
/// </summary>
public class CacheRemoveAspect : MethodInterception
{
    private string _pattern;
    private readonly ICacheManager _cacheManager;
    const string commandHandler = "CommandHandler";
    const string create = "Create";
    const string update = "Update";
    const string delete = "Delete";
    const string get = "Get";
    const string internalCommand = "Internal";
    const string register = "Register";
    public CacheRemoveAspect(string pattern = "")
    {
        _pattern = pattern;
        _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        Priority = DevArchitectureSettings.Priorities.CacheRemoveAspectPriority;
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
            targetTypeName = targetTypeName.Replace(internalCommand, string.Empty);
            targetTypeName = targetTypeName.Replace(register, string.Empty);
            _pattern = get + targetTypeName;
            var lastChar = _pattern.Substring(_pattern.Length - 1);
            if (lastChar == "y")
            {
                _pattern = _pattern.Replace(_pattern.Substring(_pattern.Length - 1), "ies");
            }
        }
        _cacheManager.RemoveByPattern(_pattern);
    }
}
