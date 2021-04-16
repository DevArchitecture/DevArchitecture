namespace DataAccess.Concrete.EntityFramework
{
    using Core.DataAccess.EntityFramework;
    using Core.Entities.Concrete;
    using DataAccess.Abstract;
    using DataAccess.Concrete.EntityFramework.Contexts;

    public class LogRepository : EfEntityRepositoryBase<Log, ProjectDbContext>, ILogRepository
	{
		public LogRepository(ProjectDbContext context)
			: base(context)
		{
		}
	}
}
