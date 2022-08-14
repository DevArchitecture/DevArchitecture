using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class GroupEntityConfiguration : BaseConfiguration<Group>
{
    public override void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(x => x.GroupName).HasMaxLength(50).IsRequired();

        builder.HasData(new Group { Id = 1, TenantId = 1, GroupName = "Users" });

        base.Configure(builder);
    }
}
