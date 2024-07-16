using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class LogRepository(ProjectDbContext context) : EfEntityRepositoryBase<Log, ProjectDbContext>(context), ILogRepository
    {
    }
}