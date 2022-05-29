using Business.Handlers.Groups.Commands;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Result> AddGroup(CreateGroupCommand createGroupCommand)
        => GetResponse(await Mediator.Send(createGroupCommand));

    public async Task<Result> UpdateGroup(UpdateGroupCommand updateGroupCommand)
        => GetResponse(await Mediator.Send(updateGroupCommand));

    public async Task<Result> DeleteGroup(DeleteGroupCommand deleteGroupCommand)
        => GetResponse(await Mediator.Send(deleteGroupCommand));
}
