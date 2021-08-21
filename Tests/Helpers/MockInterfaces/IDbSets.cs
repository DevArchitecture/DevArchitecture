using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Tests.Helpers.MockInterfaces
{
    public interface IDbSets
    {
        DbSet<OperationClaim> OperationClaims { get; set; }
        DbSet<UserClaim> UserClaims { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<UserGroup> UserGroups { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<GroupClaim> GroupClaims { get; set; }
        DbSet<Log> Logs { get; set; }
        DbSet<MobileLogin> MobileLogins { get; set; }
    }
}