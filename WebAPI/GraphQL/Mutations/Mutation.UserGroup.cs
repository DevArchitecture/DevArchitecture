using Business.Handlers.UserGroups.Commands;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Result> AddUserGroup(CreateUserGroupCommand createUserGroupCommand)
        => GetResponse(await Mediator.Send(createUserGroupCommand));

    public async Task<Result> UpdateUserGroup(UpdateUserGroupCommand updateUserGroupCommand)
        => GetResponse(await Mediator.Send(updateUserGroupCommand));

    public async Task<Result> UpdateUserGroupByGroupId(UpdateUserGroupByGroupIdCommand updateUserGroupByGroupIdCommand)
        => GetResponse(await Mediator.Send(updateUserGroupByGroupIdCommand));

    public async Task<Result> DeleteUserGroup(DeleteUserGroupCommand deleteUserGroupCommand)
        => GetResponse(await Mediator.Send(deleteUserGroupCommand));
}
