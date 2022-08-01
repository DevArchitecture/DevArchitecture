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

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetUserGroupListByUserId(GetUserGroupLookupQuery getUserGroupLookupQuery)
        => GetResponseWithResult(await Mediator.Send(getUserGroupLookupQuery));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetGroupClaimListByUserId(GetUserGroupLookupByUserIdQuery getUserGroupLookupByUserIdQuery)
        => GetResponseWithResult(await Mediator.Send(getUserGroupLookupByUserIdQuery));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetUserListInGroupByGroupId(GetUsersInGroupLookupByGroupIdQuery getUsersInGroupLookupByGroupIdQuery)
        => GetResponseWithResult(await Mediator.Send(getUsersInGroupLookupByGroupIdQuery));
}
