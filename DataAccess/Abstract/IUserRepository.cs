using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
	public interface IUserRepository : IEntityRepository<User>
	{
		List<OperationClaim> GetClaims(int userId);
	}
}