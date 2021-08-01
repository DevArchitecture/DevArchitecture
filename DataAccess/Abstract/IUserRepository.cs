namespace DataAccess.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.DataAccess;
    using Core.Entities.Concrete;

    public interface IUserRepository : IEntityRepository<User>
	{
		List<OperationClaim> GetClaims(int userId);
        Task<User> GetByRefreshToken(string refreshToken);
    }
}