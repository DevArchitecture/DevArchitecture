namespace DataAccess.Abstract
{
    using System.Collections.Generic;
    using Core.DataAccess;
    using Core.Entities.Concrete;

    public interface IUserRepository : IEntityRepository<User>
	{
		List<OperationClaim> GetClaims(int userId);
	}
}