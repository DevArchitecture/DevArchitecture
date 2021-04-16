namespace DataAccess.Concrete.Configurations
{
    using Core.Entities.Concrete;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.GroupName).HasMaxLength(50).IsRequired();
        }
    }
}
