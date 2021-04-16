namespace DataAccess.Concrete.Configurations
{
    using Core.Entities.Concrete;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserClaimEntityConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.HasKey(x => new { x.UserId, x.ClaimId });
        }
    }
}
