using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class LanguageEntityConfiguration : BaseConfiguration<Language>
    {
        public override void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.Property(x => x.Code).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(10).IsRequired();
            builder.HasData(
                new Language { Id = 1, Name = "Türkçe", Code = "tr-TR" },
                new Language { Id = 2, Name = "English", Code = "en-US" });

            base.Configure(builder);
        }
    }
}