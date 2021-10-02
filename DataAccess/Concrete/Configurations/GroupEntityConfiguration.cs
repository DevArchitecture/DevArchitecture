using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class GroupEntityConfiguration : BaseConfiguration<Group>
    {
        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(x => x.GroupName).HasMaxLength(50).IsRequired();

            base.Configure(builder);
        }
    }
}