namespace DataAccess.Concrete.Configurations
{
    using Core.Entities.Concrete;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OperationClaimEntityConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Alias).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(100);
        }
    }
}
