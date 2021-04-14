namespace DataAccess.Concrete.Configurations
{
    using Core.Entities.Concrete;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MobileLoginEntityConfiguration : IEntityTypeConfiguration<MobileLogin>
	{
		public void Configure(EntityTypeBuilder<MobileLogin> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
			builder.Property(x => x.Provider)
					.IsRequired();
			builder.Property(x => x.ExternalUserId)
					.HasMaxLength(20)
					.IsRequired();
			builder.Property(x => x.SendDate);
			builder.Property(x => x.IsSend);
			builder.Property(x => x.IsUsed);

			builder.HasIndex(x => new { x.ExternalUserId, x.Provider });
		}
	}
}
