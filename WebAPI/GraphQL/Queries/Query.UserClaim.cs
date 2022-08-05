using Business.Handlers.UserClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<UserClaim>>> GetUserClaimList()
        => GetResponseWithResult(await Mediator.Send(new GetUserClaimsQuery()));

    public async Task<DataResult<IEnumerable<UserClaim>>> GetUserClaimListByUserId(GetUserClaimLookupQuery getUserClaimLookupQuery)
        => GetResponseWithResult(await Mediator.Send(getUserClaimLookupQuery));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetOperationClaimListByUserId(GetUserClaimLookupByUserIdQuery getUserClaimLookupByUserIdQuery)
        => GetResponseWithResult(await Mediator.Send(getUserClaimLookupByUserIdQuery));
}
