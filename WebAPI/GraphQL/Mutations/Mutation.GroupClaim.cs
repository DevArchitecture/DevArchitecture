using Business.Handlers.GroupClaims.Commands;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Result> AddGroupClaim(CreateGroupClaimCommand createGroupClaimCommand)
        => GetResponse(await Mediator.Send(createGroupClaimCommand));

    public async Task<Result> UpdateGroupClaim(UpdateGroupClaimCommand updateGroupClaimCommand)
        => GetResponse(await Mediator.Send(updateGroupClaimCommand));

    public async Task<Result> DeleteGroupClaim(DeleteGroupClaimCommand deleteGroupClaimCommand)
        => GetResponse(await Mediator.Send(deleteGroupClaimCommand));
}
