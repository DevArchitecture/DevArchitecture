using Business.Handlers.OperationClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<OperationClaim>>> GetOperationClaimList()
        => GetResponseWithResult(await Mediator.Send(new GetOperationClaimsQuery()));

    public async Task<DataResult<OperationClaim>> GetOperationClaimById(GetOperationClaimQuery getOperationClaimQuery)
        => GetResponseWithResult(await Mediator.Send(getOperationClaimQuery));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetOperationClaimLookUpList()
        => GetResponseWithResult(await Mediator.Send(new GetOperationClaimLookupQuery()));

    public async Task<DataResult<IEnumerable<string>>> GetUserClaimListFromCache()
        => GetResponseWithResult(await Mediator.Send(new GetUserClaimsFromCacheQuery()));
}
