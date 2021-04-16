namespace DataAccess.Concrete.EntityFramework
{
    using Core.DataAccess.EntityFramework;
    using Core.Entities.Concrete;
    using DataAccess.Abstract;
    using DataAccess.Concrete.EntityFramework.Contexts;

    public class OperationClaimRepository : EfEntityRepositoryBase<OperationClaim, ProjectDbContext>, IOperationClaimRepository
    {
        public OperationClaimRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}
