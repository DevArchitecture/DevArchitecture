namespace DataAccess.Abstract
{
    using Core.DataAccess;
    using Core.Entities.Concrete;

    public interface ILogRepository : IEntityRepository<Log>
	{
	}
}