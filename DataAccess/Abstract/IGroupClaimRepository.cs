using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface IGroupClaimRepository : IEntityRepository<GroupClaim>
	{
		Task<IEnumerable<SelectionItem>> GetGroupClaimsSelectedList(int groupId);
		Task BulkInsert(int groupId, IEnumerable<GroupClaim> groupClaims);
	}
}
