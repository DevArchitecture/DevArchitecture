using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
	public class LogRepository : EfEntityRepositoryBase<Log, ProjectDbContext>, ILogRepository
	{
		public LogRepository(ProjectDbContext context)
			: base(context)
		{
		}
	}
}
