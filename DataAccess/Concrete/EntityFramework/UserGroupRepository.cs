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
	public class UserGroupRepository : EfEntityRepositoryBase<UserGroup, ProjectDbContext>, IUserGroupRepository
	{
		public UserGroupRepository(ProjectDbContext context) : base(context)
		{
		}

		public async Task BulkInsert(int userId, IEnumerable<UserGroup> userGroups)
		{
			var dbUserGroupList = Context.UserGroups.Where(x => x.UserId == userId);

			Context.UserGroups.RemoveRange(dbUserGroupList);
			await Context.UserGroups.AddRangeAsync(userGroups);

		}

		public async Task BulkInsertByGroupId(int groupId, IEnumerable<UserGroup> userGroups)
		{
			var dbUserGroupList = Context.UserGroups.Where(x => x.GroupId == groupId);

			Context.UserGroups.RemoveRange(dbUserGroupList);
			await Context.UserGroups.AddRangeAsync(userGroups);
		}

		public async Task<IEnumerable<SelectionItem>> GetUserGroupSelectedList(int userId)
		{
			var list = await (from grp in Context.Groups
																					join userGroup in Context.UserGroups on grp.Id equals userGroup.GroupId
																					where userGroup.UserId == userId
																					select new SelectionItem()
																					{
																						Id = grp.Id.ToString(),
																						Label = grp.GroupName
																					}).ToListAsync();

			return list;
		}

		public async Task<IEnumerable<SelectionItem>> GetUsersInGroupSelectedListByGroupId(int groupId)
		{
			var list = await (from usr in Context.Users
																					join grpUser in Context.UserGroups on usr.UserId equals grpUser.UserId
																					where grpUser.GroupId == groupId
																					select new SelectionItem()
																					{
																						Id = usr.UserId.ToString(),
																						Label = usr.FullName
																					}).ToListAsync();

			return list;
		}
	}
}
