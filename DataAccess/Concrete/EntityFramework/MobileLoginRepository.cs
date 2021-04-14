namespace DataAccess.Concrete.EntityFramework
{
    using Core.DataAccess.EntityFramework;
    using Core.Entities.Concrete;
    using DataAccess.Abstract;
    using DataAccess.Concrete.EntityFramework.Contexts;

    public class MobileLoginRepository : EfEntityRepositoryBase<MobileLogin, ProjectDbContext>, IMobileLoginRepository
	{
		public MobileLoginRepository(ProjectDbContext context)
			: base(context)
		{
		}
	}
}
