using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Settings;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Aspects.Autofac.Exception;

/// <summary>
/// ExceptionLogAspect
/// </summary>
public class ExceptionLogAspect : MethodInterception
{
    private readonly LoggerServiceBase _loggerServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExceptionLogAspect() : this(DevArchitectureSettings.Loggers.ExceptionLogAspectLogger)
    {
        Priority = DevArchitectureSettings.Priorities.ExceptionLogAspectPriority;
    }

    public ExceptionLogAspect(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new ArgumentException(AspectMessages.WrongLoggerType);
        }
        _loggerServiceBase = (LoggerServiceBase)ServiceTool.ServiceProvider.GetService(loggerService);
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    protected override void OnException(IInvocation invocation, System.Exception e)
    {
        var logDetailWithException = GetLogDetail(invocation);

        logDetailWithException.ExceptionMessage = e.InnerException is not null
            ? string.Join(Environment.NewLine, (e as AggregateException).InnerExceptions.Select(x => x.Message))
            : e.Message;
        _loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));
    }

    private LogDetailWithException GetLogDetail(IInvocation invocation)
    {
        var tenantId = _httpContextAccessor.HttpContext?.User.Claims
             .FirstOrDefault(x => x.Type.EndsWith("tenantId"))?.Value;
        var logParameters = invocation.Arguments.Select((t, i) => new LogParameter
        {
            Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
            Value = t,
            Type = t.GetType().Name
        })
            .ToList();
        var logDetailWithException = new LogDetailWithException
        {
            MethodName = invocation.Method.Name,
            Parameters = logParameters,
            User = (_httpContextAccessor.HttpContext == null ||
                    _httpContextAccessor.HttpContext.User.Identity.Name == null)
                ? "?"
                : _httpContextAccessor.HttpContext.User.Identity.Name,
            TenantId = (_httpContextAccessor.HttpContext == null ||
                    tenantId == null)
                ? "?"
                : tenantId,
        };
        return logDetailWithException;
    }
}
