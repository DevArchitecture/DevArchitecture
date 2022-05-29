using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Queries;

public class BaseQuery
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BaseQuery(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected IMediator Mediator => _httpContextAccessor.HttpContext.RequestServices.GetService<IMediator>();

    protected DataResult<T> GetResponseWithResult<T>(IDataResult<T> result)
        => result.Success
            ? new SuccessDataResult<T>(result.Data, result.Message)
            : new ErrorDataResult<T>(result.Message);
}
