using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Aspects.Autofac.Logging
{
    /// <summary>
    /// LogAspect
    /// </summary>
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new ArgumentException(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)ServiceTool.ServiceProvider.GetService(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase?.Info(GetLogDetail(invocation));            
        }

        private string GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (var i = 0; i < invocation.Arguments.Length; i++)
            {
                var parameterName = invocation.GetConcreteMethod().GetParameters()[i].Name;
                var isSensitiveParameter = SensitiveNameParts.Any(part =>
                    parameterName.Contains(part, StringComparison.OrdinalIgnoreCase));

                logParameters.Add(new LogParameter
                {
                    Name = parameterName,
                    Value = isSensitiveParameter ? "***" : MaskSensitive(invocation.Arguments[i]),
                    Type = invocation.Arguments[i].GetType().Name,
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                User = (_httpContextAccessor.HttpContext == null ||
                        _httpContextAccessor.HttpContext.User.Identity.Name == null)
                    ? "?"
                    : _httpContextAccessor.HttpContext.User.Identity.Name
            };
            return JsonConvert.SerializeObject(logDetail);
        }

        private static readonly string[] SensitiveNameParts =
        {
            "password", "securitykey", "refreshtoken", "token"
        };

        private static object MaskSensitive(object value)
        {
            if (value == null)
            {
                return null;
            }

            var token = JToken.Parse(JsonConvert.SerializeObject(value));
            Mask(token);
            return token;
        }

        private static void Mask(JToken token)
        {
            if (token is JObject obj)
            {
                foreach (var property in obj.Properties().ToList())
                {
                    if (SensitiveNameParts.Any(part => property.Name.Contains(part, StringComparison.OrdinalIgnoreCase)))
                    {
                        property.Value = "***";
                    }
                    else
                    {
                        Mask(property.Value);
                    }
                }
            }
            else if (token is JArray array)
            {
                foreach (var item in array)
                {
                    Mask(item);
                }
            }
        }
    }
}