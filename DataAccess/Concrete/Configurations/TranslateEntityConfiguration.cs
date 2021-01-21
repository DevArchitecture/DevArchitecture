using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccess.Concrete.Configurations
{
	public class TranslateEntityConfiguration : IEntityTypeConfiguration<Translate>
	{
		public void Configure(EntityTypeBuilder<Translate> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.LangId).IsRequired();
			builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
			builder.Property(x => x.Value).HasMaxLength(50).IsRequired();
			builder.HasData(
				new Translate { Id = 1, LangId = 1, Code = "LOGIN", Value = "Giriş TR" },
				new Translate { Id = 2, LangId = 1, Code = "email", Value = "E posta" },
				new Translate { Id = 3, LangId = 1, Code = "password", Value = "Şifre" },
				new Translate { Id = 4, LangId = 1, Code = "update", Value = "Güncelle" },
				new Translate { Id = 5, LangId = 1, Code = "delete", Value = "Sil" },
				new Translate { Id = 6, LangId = 1, Code = "users_Groups", Value = "Kullanıcının Grupları" },
				new Translate { Id = 7, LangId = 1, Code = "users_claims", Value = "Kullanıcının Yetkileri" },
				new Translate { Id = 8, LangId = 1, Code = "create", Value = "Yeni" },
				new Translate { Id = 9, LangId = 1, Code = "Users", Value = "Kullanıcılar" },
				new Translate { Id = 10, LangId = 1, Code = "Groups", Value = "Gruplar" },
				new Translate { Id = 11, LangId = 2, Code = "LOGIN", Value = "Login EN" },
				new Translate { Id = 12, LangId = 2, Code = "email", Value = "Email" },
				new Translate { Id = 13, LangId = 2, Code = "password", Value = "Password" },
				new Translate { Id = 14, LangId = 2, Code = "update", Value = "Update" },
				new Translate { Id = 15, LangId = 2, Code = "delete", Value = "Delete" },
				new Translate { Id = 16, LangId = 2, Code = "users_Groups", Value = "User's Groups" },
				new Translate { Id = 17, LangId = 2, Code = "users_claims", Value = "User's Claims" },
				new Translate { Id = 18, LangId = 2, Code = "create", Value = "Create" },
				new Translate { Id = 19, LangId = 2, Code = "Users", Value = "Users" },
				new Translate { Id = 20, LangId = 2, Code = "Groups", Value = "Groups" },
				new Translate { Id = 21, LangId = 1, Code = "OperationClaim", Value = "Operasyon Yetkileri" },
				new Translate { Id = 22, LangId = 2, Code = "OperationClaim", Value = "Operation Claim" },
				new Translate { Id = 23, LangId = 1, Code = "Languages", Value = "Diller" },
				new Translate { Id = 24, LangId = 2, Code = "Languages", Value = "Languages" },
				new Translate { Id = 25, LangId = 1, Code = "TranslateWords", Value = "Dil Çevirileri" },
				new Translate { Id = 26, LangId = 2, Code = "TranslateWords", Value = "Translate Words" },
				new Translate { Id = 27, LangId = 1, Code = "Management", Value = "Yönetim" },
				new Translate { Id = 28, LangId = 2, Code = "Management", Value = "Management" },
				new Translate { Id = 29, LangId = 1, Code = "AppMenu", Value = "Uygulama" },
				new Translate { Id = 30, LangId = 2, Code = "AppMenu", Value = "Application" });
		}
	}
}
