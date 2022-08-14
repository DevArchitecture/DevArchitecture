using AutoMapper;
using Business.Handlers.OperationClaims.Queries;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using static Business.Handlers.OperationClaims.Queries.GetOperationClaimsQuery;

namespace Business.Fakes.Handlers.OperationClaims;

public class GetOperationClaimsInternalQuery : IRequest<IDataResult<IEnumerable<OperationClaim>>>
{
    public class GetOperationClaimsInternalQueryHandler : IRequestHandler<GetOperationClaimsInternalQuery,
        IDataResult<IEnumerable<OperationClaim>>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetOperationClaimsInternalQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        [LogAspect]
        public async Task<IDataResult<IEnumerable<OperationClaim>>> Handle(GetOperationClaimsInternalQuery request, CancellationToken cancellationToken)
        {
            var handler = new GetOperationClaimsQueryHandler(_operationClaimRepository);
            return await handler.Handle(_mapper.Map<GetOperationClaimsQuery>(request), cancellationToken);
        }
    }
}
