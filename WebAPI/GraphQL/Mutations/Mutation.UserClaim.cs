using Business.Handlers.UserClaims.Commands;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Result> AddUserClaim(CreateUserClaimCommand createUserClaimCommand)
        => GetResponse(await Mediator.Send(createUserClaimCommand));

    public async Task<Result> UpdateUserClaim(UpdateUserClaimCommand updateUserClaimCommand)
        => GetResponse(await Mediator.Send(updateUserClaimCommand));

    public async Task<Result> DeleteUserClaim(DeleteUserClaimCommand deleteUserClaimCommand)
        => GetResponse(await Mediator.Send(deleteUserClaimCommand));
}
