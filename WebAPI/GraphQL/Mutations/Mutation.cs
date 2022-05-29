namespace WebAPI.GraphQL.Mutations;

public partial class Mutation : BaseMutation
{
    public Mutation(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }
}
