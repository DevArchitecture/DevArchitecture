using Business.Handlers.Groups.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<Group>>> GetGroupList()
        => GetResponseWithResult(await Mediator.Send(new GetGroupsQuery()));

    public async Task<DataResult<Group>> GetGroupById(GetGroupQuery getGroupQuery)
        => GetResponseWithResult(await Mediator.Send(getGroupQuery));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetGroupLookupList()
        => GetResponseWithResult(await Mediator.Send(new GetGroupLookupQuery()));
}
