using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserRepository : EfEntityRepositoryBase<User, ProjectDbContext>, IUserRepository
    {
        public UserRepository(ProjectDbContext context) : base(context)
        {
        }

        public List<OperationClaim> GetClaims(int userId)
        {
            var result = (from user in context.Users
                          join userGroup in context.UserGroups on user.UserId equals userGroup.UserId
                          join groupClaim in context.GroupClaims on userGroup.GroupId equals groupClaim.GroupId
                          join operationClaim in context.OperationClaims on groupClaim.ClaimId equals operationClaim.Id
                          where user.UserId == userId
                          select new
                          {
                              operationClaim.Name
                          }).
                                        Union(from user in context.Users
                                              join userClaim in context.UserClaims on user.UserId equals userClaim.UserId
                                              join operationClaim in context.OperationClaims on userClaim.ClaimId equals operationClaim.Id
                                              where user.UserId == userId

                                              select new
                                              {
                                                  operationClaim.Name
                                              }
                                );
            return result.Select(x => new OperationClaim { Name = x.Name }).Distinct()
                    .ToList();
        }
    }
}