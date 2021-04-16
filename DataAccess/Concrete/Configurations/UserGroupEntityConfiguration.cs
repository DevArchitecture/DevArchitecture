namespace DataAccess.Concrete.Configurations
{
    using Core.Entities.Concrete;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserGroupEntityConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(x => new { x.UserId, x.GroupId });
        }
    }
}
