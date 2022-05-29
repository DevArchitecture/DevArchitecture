using Core.Utilities.Results;
using MediatR;
using IResult = Core.Utilities.Results.IResult;
namespace WebAPI.GraphQL.Mutations;

public class BaseMutation
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BaseMutation(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected IMediator Mediator => _httpContextAccessor.HttpContext.RequestServices.GetService<IMediator>();
    protected Result GetResponse(IResult result)
        => result.Success
        ? new SuccessResult(result.Message)
        : new ErrorResult(result.Message);
}
