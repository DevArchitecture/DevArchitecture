using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Business.BusinessAspects
{
    /// <summary>
    /// Validation, performance ve error loglama işlerini yapan ve mediatr handler'larını saran
    /// native MediatR pipeline handlerıdır.
    /// 
    /// Exceptionlar buradan işlenip tekrar raise edildikten sonra Core.ExceptionMiddleware
    /// tarafından da işlenmeye devam eder.
    /// 
    /// SOLID'in S'sine pek uygmuyor fakat istenirse 3 pipeline'a ayrılabilir.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class InstrumentationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IActivityMonitor _monitor;
        private readonly ILogger _logger;
        private readonly IPrincipal _principal;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public InstrumentationBehavior(IActivityMonitor monitor, ILogger<TRequest> logger, IPrincipal principal,

          IEnumerable<IValidator<TRequest>> validators)
        {
            _monitor = monitor;
            _logger = logger;
            _principal = principal;
            _validators = validators;
        }

        protected void Tick(string actionName, int ticks = 1, long msecs = 0)
        {
            if (_principal != null)
                _monitor.Tick(actionName, 1, msecs, "User." + (_principal.Identity.Name ?? "?"));
            else
                _monitor.Tick(actionName, 1, msecs);
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
          RequestHandlerDelegate<TResponse> next)
        {
            Type type = request.GetType();
            using (var pr = new Profiler())
            {
                var shortName = type.ReflectedType?.Name + "." + type.Name;

                pr.Begin("Handler " + type.FullName);

                try
                {
                    /*
                                        var failures = _validators
                                          .Select(v => v.Validate(request))
                                          .SelectMany(result => result.Errors)
                                          .Where(f => f != null)
                                          .ToList();

                                        if (failures.Count > 0)
                                        {

                                            var arr = failures.Select(x => $"{Environment.NewLine} -- {x.PropertyName}: {x.ErrorMessage}");
                                            throw new ValidationException("Lütfen hataları düzeltiniz:" + string.Join(string.Empty, arr));
                                        }
                    */
                    var response = await next();

                    pr.End();
                    Tick(shortName, 1, pr.LastMsecs);

                    return response;
                }
                catch (Exception ex)
                {
                    pr.End();
                    Tick(shortName, 1, pr.LastMsecs);

                    var sb = new StringBuilder();

                    sb.AppendLine(request.GetType().Name);
                    sb.AppendLine(JsonSerializer.Serialize(request));


                    sb.AppendLine(ex.ToString());
                    _logger.LogError(sb.ToString());
                    throw;
                }
            }
        }
    }

}