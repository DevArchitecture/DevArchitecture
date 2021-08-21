using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserRepository : EfEntityRepositoryBase<User, ProjectDbContext>, IUserRepository
    {
        public UserRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public List<OperationClaim> GetClaims(int userId)
        {
            var result = (from user in Context.Users
                join userGroup in Context.UserGroups on user.UserId equals userGroup.UserId
                join groupClaim in Context.GroupClaims on userGroup.GroupId equals groupClaim.GroupId
                join operationClaim in Context.OperationClaims on groupClaim.ClaimId equals operationClaim.Id
                where user.UserId == userId
                select new
                {
                    operationClaim.Name
                }).Union(from user in Context.Users
                join userClaim in Context.UserClaims on user.UserId equals userClaim.UserId
                join operationClaim in Context.OperationClaims on userClaim.ClaimId equals operationClaim.Id
                where user.UserId == userId
                select new
                {
                    operationClaim.Name
                });

            return result.Select(x => new OperationClaim { Name = x.Name }).Distinct()
                .ToList();
        }

        public async Task<User> GetByRefreshToken(string refreshToken)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.Status);
        }
    }
}