using Business.Handlers.Users.Commands;
using Core.Utilities.Results;

namespace WebAPI.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Result> AddUser(CreateUserCommand createUserCommand)
        => GetResponse(await Mediator.Send(createUserCommand));

    public async Task<Result> UpdateUser(UpdateUserCommand updateUserCommand)
        => GetResponse(await Mediator.Send(updateUserCommand));

    public async Task<Result> DeleteUser(DeleteUserCommand deleteUserCommand)
        => GetResponse(await Mediator.Send(deleteUserCommand));
}
