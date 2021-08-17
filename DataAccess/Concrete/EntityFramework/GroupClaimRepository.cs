using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class GroupClaimRepository : EfEntityRepositoryBase<GroupClaim, ProjectDbContext>, IGroupClaimRepository
    {
        public GroupClaimRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task BulkInsert(int groupId, IEnumerable<GroupClaim> groupClaims)
        {
            var DbList = await Context.GroupClaims.Where(x => x.GroupId == groupId).ToListAsync();

            Context.GroupClaims.RemoveRange(DbList);
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