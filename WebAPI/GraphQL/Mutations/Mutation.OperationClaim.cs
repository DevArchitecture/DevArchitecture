using Business.Handlers.OperationClaims.Commands;
using Core.Utilities.Results;

namespace WebAPI.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Result> UpdateOperationClaim(UpdateOperationClaimCommand updateOperationClaimCommand)
        => GetResponse(await Mediator.Send(updateOperationClaimCommand));
}
