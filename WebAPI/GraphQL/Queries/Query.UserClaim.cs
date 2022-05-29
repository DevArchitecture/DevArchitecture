using Business.Handlers.UserClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<UserClaim>>> GetUserClaimList()
        => GetResponseWithResult(await Mediator.Send(new GetUserClaimsQuery()));

    public async Task<DataResult<IEnumerable<UserClaim>>> GetUserClaimListByUserId(int userId)
        => GetResponseWithResult(await Mediator.Send(new GetUserClaimLookupQuery { UserId = userId }));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetOperationClaimListByUserId(int id)
        => GetResponseWithResult(await Mediator.Send(new GetUserClaimLookupByUserIdQuery { Id = id }));
}
