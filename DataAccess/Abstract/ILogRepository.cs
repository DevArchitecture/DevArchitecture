using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ILogRepository : IEntityRepository<Log>
    {
    }
}