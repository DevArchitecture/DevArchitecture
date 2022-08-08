using Business.Handlers.Logs.Queries;
using Core.Entities.Dtos;
using Core.Utilities.Results;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<LogDto>>> GetLogList()
        => GetResponseWithResult(await Mediator.Send(new GetLogDtoQuery()));
}
