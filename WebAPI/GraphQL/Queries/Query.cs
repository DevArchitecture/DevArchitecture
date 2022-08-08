namespace WebAPI.GraphQL.Queries;

public partial class Query : BaseQuery
{
    public Query(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }
}
