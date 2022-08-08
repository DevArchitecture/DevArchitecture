using Business.Handlers.GroupClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<GroupClaim>>> GetGroupClaimList()
        => GetResponseWithResult(await Mediator.Send(new GetGroupClaimsQuery()));

    public async Task<DataResult<GroupClaim>> GetGroupClaimById(GetGroupClaimQuery getGroupClaimQuery)
        => GetResponseWithResult(await Mediator.Send(getGroupClaimQuery));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetGroupClaimListByGroupId(GetGroupClaimsLookupByGroupIdQuery getGroupClaimsLookupByGroupIdQuery)
        => GetResponseWithResult(await Mediator.Send(getGroupClaimsLookupByGroupIdQuery));
}
