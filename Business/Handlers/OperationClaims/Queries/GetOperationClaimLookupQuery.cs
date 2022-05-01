using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.OperationClaims.Queries;

public class GetOperationClaimLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
{
    public class GetOperationClaimLookupQueryHandler : IRequestHandler<GetOperationClaimLookupQuery,
        IDataResult<IEnumerable<SelectionItem>>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public GetOperationClaimLookupQueryHandler(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        [SecuredOperation(Priority = 1)]
        [LogAspect()]
        public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetOperationClaimLookupQuery request, CancellationToken cancellationToken)
        {
            var list = await _operationClaimRepository.GetListAsync();

            var operationClaim = list.Select(x => new SelectionItem()
            {
                Id = x.Id.ToString(),
                Label = x.Alias ?? x.Name
            });
            return new SuccessDataResult<IEnumerable<SelectionItem>>(
                operationClaim);
        }
    }
}
