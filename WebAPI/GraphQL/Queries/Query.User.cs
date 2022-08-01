using Business.Handlers.Users.Queries;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<UserDto>>> GetUserList()
        => GetResponseWithResult(await Mediator.Send(new GetUsersQuery()));

    public async Task<DataResult<UserDto>> GetUserById(GetUserQuery getUserQuery)
        => GetResponseWithResult(await Mediator.Send(getUserQuery));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetUserLookUpList()
        => GetResponseWithResult(await Mediator.Send(new GetUserLookupQuery()));
}
