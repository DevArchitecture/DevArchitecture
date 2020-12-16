using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Core.Entities;

namespace DataAccess.Concrete.EntityFramework
{
	public class MobileLoginRepository : EfEntityRepositoryBase<MobileLogin, ProjectDbContext>, IMobileLoginRepository
	{
		public MobileLoginRepository(ProjectDbContext context) : base(context)
		{
		}
	}
}
