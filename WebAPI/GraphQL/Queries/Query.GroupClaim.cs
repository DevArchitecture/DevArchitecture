using Business.Handlers.GroupClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<GroupClaim>>> GetGroupClaimList()
        => GetResponseWithResult(await Mediator.Send(new GetGroupClaimsQuery()));

    public async Task<DataResult<GroupClaim>> GetGroupClaimById(int id)
        => GetResponseWithResult(await Mediator.Send(new GetGroupClaimQuery { Id = id }));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetGroupClaimListByGroupId(int id)
        => GetResponseWithResult(await Mediator.Send(new GetGroupClaimsLookupByGroupIdQuery { GroupId = id }));
}
