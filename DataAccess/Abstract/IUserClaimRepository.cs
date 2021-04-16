namespace DataAccess.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.DataAccess;
    using Core.Entities.Concrete;
    using Core.Entities.Dtos;

    public interface IUserClaimRepository : IEntityRepository<UserClaim>
	{
		Task<IEnumerable<SelectionItem>> GetUserClaimSelectedList(int userId);
		Task<IEnumerable<UserClaim>> BulkInsert(int userId, IEnumerable<UserClaim> userClaims);
	}
}
