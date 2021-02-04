using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Core.Aspects.Autofac.Exception
{
	/// <summary>
	/// ExceptionLogAspect
	/// </summary>
	public class ExceptionLogAspect : MethodInterception
	{
		private readonly LoggerServiceBase _loggerServiceBase;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ExceptionLogAspect(Type loggerService)
		{
			if (loggerService.BaseType != typeof(LoggerServiceBase))
			{
				throw new ArgumentException(AspectMessages.WrongLoggerType);
			}

			_loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
			_httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

		}

		protected override void OnException(IInvocation invocation, System.Exception e)
		{
			var logDetailWithException = GetLogDetail(invocation);

			if (e is AggregateException)
				logDetailWithException.ExceptionMessage =
					string.Join(Environment.NewLine, (e as AggregateException).InnerExceptions.Select(x => x.Message));
			else
				logDetailWithException.ExceptionMessage = e.Message;
			_loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));
		}

		private LogDetailWithException GetLogDetail(IInvocation invocation)
		{
			var logParameters = new List<LogParameter>();
			for (var i = 0; i < invocation.Arguments.Length; i++)
			{
				logParameters.Add(new LogParameter
				{
					Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
					Value = invocation.Arguments[i],
					Type = invocation.Arguments[i].GetType().Name
				});
			}
			var logDetailWithException = new LogDetailWithException
			{
				MethodName = invocation.Method.Name,
				Parameters = logParameters,
				User = (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User.Identity.Name == null) ? "?" : _httpContextAccessor.HttpContext.User.Identity.Name
			};
			return logDetailWithException;
		}
	}
}
