using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Fakes.Handlers.OperationClaims
{
    public class GetOperationClaimsInternalQuery : IRequest<IDataResult<IEnumerable<OperationClaim>>>
	{
        public class GetOperationClaimsInternalQueryHandler : IRequestHandler<GetOperationClaimsInternalQuery, IDataResult<IEnumerable<OperationClaim>>>
		{
			private readonly IOperationClaimRepository _operationClaimRepository;

			public GetOperationClaimsInternalQueryHandler(IOperationClaimRepository operationClaimRepository)
			{
				_operationClaimRepository = operationClaimRepository;
			}
            [LogAspect(typeof(FileLogger))]
			public async Task<IDataResult<IEnumerable<OperationClaim>>> Handle(GetOperationClaimsInternalQuery request, CancellationToken cancellationToken)
			{
				return new SuccessDataResult<IEnumerable<OperationClaim>>(await _operationClaimRepository.GetListAsync());
			}
		}
	}
}
