using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

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