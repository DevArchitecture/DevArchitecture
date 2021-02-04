﻿using Core.DataAccess.EntityFramework;
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
	public class GroupClaimRepository : EfEntityRepositoryBase<GroupClaim, ProjectDbContext>, IGroupClaimRepository
	{
		public GroupClaimRepository(ProjectDbContext context) : base(context)
		{
		}

		public async Task BulkInsert(int groupId, IEnumerable<GroupClaim> groupClaims)
		{
			var dbList = await Context.GroupClaims.Where(x => x.GroupId == groupId).ToListAsync();

			Context.GroupClaims.RemoveRange(dbList);
			await Context.GroupClaims.AddRangeAsync(groupClaims);
		}

		public async Task<IEnumerable<SelectionItem>> GetGroupClaimsSelectedList(int groupId)
		{
			var list = await (from gc in Context.GroupClaims
																					join oc in Context.OperationClaims on gc.ClaimId equals oc.Id
																					where gc.GroupId == groupId
																					select new SelectionItem()
																					{
																						Id = oc.Id.ToString(),
																						Label = oc.Name
																					}).ToListAsync();

			return list;
		}
	}
}
