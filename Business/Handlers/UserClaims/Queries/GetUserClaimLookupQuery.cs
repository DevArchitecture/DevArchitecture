using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserClaims.Queries
{
    public class GetUserClaimLookupQuery : IRequest<IDataResult<IEnumerable<UserClaim>>>
    {
        public int UserId { get; set; }

        public class
            GetUserClaimQueryHandler : IRequestHandler<GetUserClaimLookupQuery, IDataResult<IEnumerable<UserClaim>>>
        {
            private readonly IUserClaimRepository _userClaimRepository;

            public GetUserClaimQueryHandler(IUserClaimRepository userClaimRepository)
            {
                _userClaimRepository = userClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<UserClaim>>> Handle(GetUserClaimLookupQuery request, CancellationToken cancellationToken)
            {
                var userClaims = await _userClaimRepository.GetListAsync(x => x.UserId == request.UserId);

                return new SuccessDataResult<IEnumerable<UserClaim>>(userClaims.ToList());
            }
        }
    }
}