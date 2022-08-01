using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Core.Aspects.Autofac.Caching;

/// <summary>
/// CacheAspect
/// </summary>
public class CacheAspect : MethodInterception
{
    private readonly int _duration;
    private readonly ICacheManager _cacheManager;
    
    public CacheAspect( int duration = 60)
    {
        _duration = duration;
        _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        
    }

    public override void Intercept(IInvocation invocation)
    {
        IHttpContextAccessor _httpContextAccessor=new HttpContextAccessor();
        var tenantId = _httpContextAccessor.HttpContext?.User.Claims
             .FirstOrDefault(x => x.Type.EndsWith("tenantId"))?.Value;
        var methodName = string.Format($"{invocation.Arguments[0]}.{invocation.Method.Name}");
        var arguments = invocation.Arguments;
        var key = $"{tenantId}{methodName}({BuildKey(arguments)})";
        var returnType = invocation.Method.ReturnType.GenericTypeArguments.FirstOrDefault();
        if (_cacheManager.IsAdd(key))
        {
            invocation.ReturnValue = _cacheManager.Get(key, returnType);
            return;
        }

        invocation.Proceed();
        _cacheManager.Add(key, invocation.ReturnValue, _duration, returnType);
    }

    static string BuildKey(object[] args)
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
