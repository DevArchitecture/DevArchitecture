using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Core.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
	public class UserClaimRepository : EfEntityRepositoryBase<UserClaim, ProjectDbContext>, IUserClaimRepository
	{
		public UserClaimRepository(ProjectDbContext context)
			: base(context)
		{
		}

		public async Task<IEnumerable<UserClaim>> BulkInsert(int userId, IEnumerable<UserClaim> userClaims)
		{
			var dbClaimList = Context.UserClaims.Where(x => x.UserId == userId);

			Context.UserClaims.RemoveRange(dbClaimList);
			await Context.UserClaims.AddRangeAsync(userClaims);
			return userClaims;
		}

		public async Task<IEnumerable<SelectionItem>> GetUserClaimSelectedList(int userId)
		{
			var list = await (from oc in Context.OperationClaims
																					join userClaims in Context.UserClaims on oc.Id equals userClaims.ClaimId
																					where userClaims.UserId == userId
																					select new SelectionItem()
																					{
																						Id = oc.Id.ToString(),
																						Label = oc.Name
																					}).ToListAsync();

			return list;
		}
	}
}
