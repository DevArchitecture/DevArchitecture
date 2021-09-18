using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class MobileLoginEntityConfiguration : BaseConfiguration<MobileLogin>
    {
        public override void Configure(EntityTypeBuilder<MobileLogin> builder)
        {
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

            base.Configure(builder);
        }
    }
}