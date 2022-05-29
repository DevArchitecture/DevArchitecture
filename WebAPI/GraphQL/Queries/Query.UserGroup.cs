using Business.Handlers.UserGroups.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<UserGroup>>> GetUserGroupList()
        => GetResponseWithResult(await Mediator.Send(new GetUserGroupsQuery()));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetUserGroupListByUserId(int userId)
        => GetResponseWithResult(await Mediator.Send(new GetUserGroupLookupQuery { UserId = userId }));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetGroupClaimListByUserId(int id)
        => GetResponseWithResult(await Mediator.Send(new GetUserGroupLookupByUserIdQuery { UserId = id }));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetUserListInGroupByGroupId(int id)
        => GetResponseWithResult(await Mediator.Send(new GetUsersInGroupLookupByGroupIdQuery { GroupId = id }));
}
