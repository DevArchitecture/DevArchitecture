using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security;

namespace Business.Handlers.OperationClaims.Queries;

public class GetUserClaimsFromCacheQuery : IRequest<IDataResult<IEnumerable<string>>>
{
    public class GetUserClaimsFromCacheQueryHandler : IRequestHandler<GetUserClaimsFromCacheQuery,
            IDataResult<IEnumerable<string>>>
    {
        private readonly ICacheManager _cacheManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetUserClaimsFromCacheQueryHandler(ICacheManager cacheManager, IHttpContextAccessor contextAccessor)
        {
            _cacheManager = cacheManager;
            _contextAccessor = contextAccessor;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect()]
        // TODO:[SecuredOperation(Priority = 1)]
        public async Task<IDataResult<IEnumerable<string>>> Handle(GetUserClaimsFromCacheQuery request, CancellationToken cancellationToken)
        {

            var userId = _contextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

            if (userId == null)
            {
                throw new SecurityException(Messages.AuthorizationsDenied);
            }
            var oprClaims = await Task.Run(() => _cacheManager.Get($"{CacheKeys.UserIdForClaim}={userId}") as IEnumerable<string>);

            return new SuccessDataResult<IEnumerable<string>>(oprClaims);
        }
    }
}
