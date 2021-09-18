using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DataAccess.Concrete.Configurations
{
    public class TranslateEntityConfiguration : BaseConfiguration<Translate>
    {
        public override void Configure(EntityTypeBuilder<Translate> builder)
        {
            builder.Property(x => x.LangId).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Value).HasMaxLength(500).IsRequired();
            builder.HasData(GetTranslates());

            base.Configure(builder);
        }

        private List<Translate> GetTranslates()
        {
            return new List<Translate>()
            {
               new Translate { Id = 1, LangId = 1, Code = "Login", Value = "Giriş" },
                new Translate { Id = 2, LangId = 1, Code = "Email", Value = "E posta" },
                new Translate { Id = 3, LangId = 1, Code = "Password", Value = "Parola" },
                new Translate { Id = 4, LangId = 1, Code = "Update", Value = "Güncelle" },
                new Translate { Id = 5, LangId = 1, Code = "Delete", Value = "Sil" },
                new Translate { Id = 6, LangId = 1, Code = "UsersGroups", Value = "Kullanıcının Grupları" },
                new Translate { Id = 7, LangId = 1, Code = "UsersClaims", Value = "Kullanıcının Yetkileri" },
                new Translate { Id = 8, LangId = 1, Code = "Create", Value = "Yeni" },
                new Translate { Id = 9, LangId = 1, Code = "Users", Value = "Kullanıcılar" },
                new Translate { Id = 10, LangId = 1, Code = "Groups", Value = "Gruplar" },
                new Translate { Id = 11, LangId = 2, Code = "Login", Value = "Login" },
                new Translate { Id = 12, LangId = 2, Code = "Email", Value = "Email" },
                new Translate { Id = 13, LangId = 2, Code = "Password", Value = "Password" },
                new Translate { Id = 14, LangId = 2, Code = "Update", Value = "Update" },
                new Translate { Id = 15, LangId = 2, Code = "Delete", Value = "Delete" },
                new Translate { Id = 16, LangId = 2, Code = "UsersGroups", Value = "User's Groups" },
                new Translate { Id = 17, LangId = 2, Code = "UsersClaims", Value = "User's Claims" },
                new Translate { Id = 18, LangId = 2, Code = "Create", Value = "Create" },
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
                new Translate { Id = 30, LangId = 2, Code = "AppMenu", Value = "Application" },
                new Translate { Id = 31, LangId = 1, Code = "Added", Value = "Başarıyla Eklendi." },
                new Translate { Id = 32, LangId = 2, Code = "Added", Value = "Successfully Added." },
                new Translate { Id = 33, LangId = 1, Code = "Updated", Value = "Başarıyla Güncellendi." },
                new Translate { Id = 34, LangId = 2, Code = "Updated", Value = "Successfully Updated." },
                new Translate { Id = 35, LangId = 1, Code = "Deleted", Value = "Başarıyla Silindi." },
                new Translate { Id = 36, LangId = 2, Code = "Deleted", Value = "Successfully Deleted." },
                new Translate
                { Id = 37, LangId = 1, Code = "OperationClaimExists", Value = "Bu operasyon izni zaten mevcut." },
                new Translate
                {
                    Id = 38,
                    LangId = 2,
                    Code = "OperationClaimExists",
                    Value = "This operation permit already exists."
                },
                new Translate
                {
                    Id = 39,
                    LangId = 1,
                    Code = "StringLengthMustBeGreaterThanThree",
                    Value = "Lütfen En Az 3 Karakterden Oluşan Bir İfade Girin."
                },
                new Translate
                {
                    Id = 40,
                    LangId = 2,
                    Code = "StringLengthMustBeGreaterThanThree",
                    Value = "Please Enter A Phrase Of At Least 3 Characters."
                },
                new Translate { Id = 41, LangId = 1, Code = "CouldNotBeVerifyCid", Value = "Kimlik No Doğrulanamadı." },
                new Translate
                { Id = 42, LangId = 2, Code = "CouldNotBeVerifyCid", Value = "Could not be verify Citizen Id" },
                new Translate { Id = 43, LangId = 1, Code = "VerifyCid", Value = "Kimlik No Doğrulandı." },
                new Translate { Id = 44, LangId = 2, Code = "VerifyCid", Value = "Verify Citizen Id" },
                new Translate
                {
                    Id = 45,
                    LangId = 1,
                    Code = "AuthorizationsDenied",
                    Value = "Yetkiniz olmayan bir alana girmeye çalıştığınız tespit edildi."
                },
                new Translate
                {
                    Id = 46,
                    LangId = 2,
                    Code = "AuthorizationsDenied",
                    Value =
                        "It has been detected that you are trying to enter an area that you do not have authorization."
                },
                new Translate
                {
                    Id = 47,
                    LangId = 1,
                    Code = "UserNotFound",
                    Value = "Kimlik Bilgileri Doğrulanamadı. Lütfen Yeni Kayıt Ekranını kullanın."
                },
                new Translate
                {
                    Id = 48,
                    LangId = 2,
                    Code = "UserNotFound",
                    Value = "Credentials Could Not Verify. Please use the New Registration Screen."
                },
                new Translate
                {
                    Id = 49,
                    LangId = 1,
                    Code = "PasswordError",
                    Value = "Kimlik Bilgileri Doğrulanamadı, Kullanıcı adı ve/veya parola hatalı."
                },
                new Translate
                {
                    Id = 50,
                    LangId = 2,
                    Code = "PasswordError",
                    Value = "Credentials Failed to Authenticate, Username and / or password incorrect."
                },
                new Translate { Id = 51, LangId = 1, Code = "SuccessfulLogin", Value = "Sisteme giriş başarılı." },
                new Translate
                { Id = 52, LangId = 2, Code = "SuccessfulLogin", Value = "Login to the system is successful." },
                new Translate
                {
                    Id = 53,
                    LangId = 1,
                    Code = "SendMobileCode",
                    Value = "Lütfen Size SMS Olarak Gönderilen Kodu Girin!"
                },
                new Translate
                {
                    Id = 54,
                    LangId = 2,
                    Code = "SendMobileCode",
                    Value = "Please Enter The Code Sent To You By SMS!"
                },
                new Translate
                {
                    Id = 55,
                    LangId = 1,
                    Code = "NameAlreadyExist",
                    Value = "Oluşturmaya Çalıştığınız Nesne Zaten Var."
                },
                new Translate
                {
                    Id = 56,
                    LangId = 2,
                    Code = "NameAlreadyExist",
                    Value = "The Object You Are Trying To Create Already Exists."
                },
                new Translate
                {
                    Id = 57,
                    LangId = 1,
                    Code = "WrongCID",
                    Value = "Vatandaşlık No Sistemimizde Bulunamadı. Lütfen Yeni Kayıt Oluşturun!"
                },
                new Translate
                {
                    Id = 58,
                    LangId = 2,
                    Code = "WrongCID",
                    Value = "Citizenship Number Not Found In Our System. Please Create New Registration!"
                },
                new Translate { Id = 59, LangId = 1, Code = "CID", Value = "Vatandaşlık No" },
                new Translate { Id = 60, LangId = 2, Code = "CID", Value = "Citizenship Number" },
                new Translate { Id = 61, LangId = 1, Code = "PasswordEmpty", Value = "Parola boş olamaz!" },
                new Translate { Id = 62, LangId = 2, Code = "PasswordEmpty", Value = "Password can not be empty!" },
                new Translate
                {
                    Id = 63,
                    LangId = 1,
                    Code = "PasswordLength",
                    Value = "Minimum 8 Karakter Uzunluğunda Olmalıdır!"
                },
                new Translate
                { Id = 64, LangId = 2, Code = "PasswordLength", Value = "Must be at least 8 characters long! " },
                new Translate
                {
                    Id = 65,
                    LangId = 1,
                    Code = "PasswordUppercaseLetter",
                    Value = "En Az 1 Büyük Harf İçermelidir!"
                },
                new Translate
                {
                    Id = 66,
                    LangId = 2,
                    Code = "PasswordUppercaseLetter",
                    Value = "Must Contain At Least 1 Capital Letter!"
                },
                new Translate
                {
                    Id = 67,
                    LangId = 1,
                    Code = "PasswordLowercaseLetter",
                    Value = "En Az 1 Küçük Harf İçermelidir!"
                },
                new Translate
                {
                    Id = 68,
                    LangId = 2,
                    Code = "PasswordLowercaseLetter",
                    Value = "Must Contain At Least 1 Lowercase Letter!"
                },
                new Translate { Id = 69, LangId = 1, Code = "PasswordDigit", Value = "En Az 1 Rakam İçermelidir!" },
                new Translate
                { Id = 70, LangId = 2, Code = "PasswordDigit", Value = "It Must Contain At Least 1 Digit!" },
                new Translate
                { Id = 71, LangId = 1, Code = "PasswordSpecialCharacter", Value = "En Az 1 Simge İçermelidir!" },
                new Translate
                {
                    Id = 72,
                    LangId = 2,
                    Code = "PasswordSpecialCharacter",
                    Value = "Must Contain At Least 1 Symbol!"
                },
                new Translate
                {
                    Id = 73,
                    LangId = 1,
                    Code = "SendPassword",
                    Value = "Yeni Parolanız E-Posta Adresinize Gönderildi."
                },
                new Translate
                {
                    Id = 74,
                    LangId = 2,
                    Code = "SendPassword",
                    Value = "Your new password has been sent to your e-mail address."
                },
                new Translate { Id = 75, LangId = 1, Code = "InvalidCode", Value = "Geçersiz Bir Kod Girdiniz!" },
                new Translate { Id = 76, LangId = 2, Code = "InvalidCode", Value = "You Entered An Invalid Code!" },
                new Translate
                { Id = 77, LangId = 1, Code = "SmsServiceNotFound", Value = "SMS Servisine Ulaşılamıyor." },
                new Translate
                { Id = 78, LangId = 2, Code = "SmsServiceNotFound", Value = "Unable to Reach SMS Service." },
                new Translate
                {
                    Id = 79,
                    LangId = 1,
                    Code = "TrueButCellPhone",
                    Value = "Bilgiler doğru. Cep telefonu gerekiyor."
                },
                new Translate
                {
                    Id = 80,
                    LangId = 2,
                    Code = "TrueButCellPhone",
                    Value = "The information is correct. Cell phone is required."
                },
                new Translate
                { Id = 81, LangId = 1, Code = "TokenProviderException", Value = "Token Provider boş olamaz!" },
                new Translate
                { Id = 82, LangId = 2, Code = "TokenProviderException", Value = "Token Provider cannot be empty!" },
                new Translate { Id = 83, LangId = 1, Code = "Unknown", Value = "Bilinmiyor!" },
                new Translate { Id = 84, LangId = 2, Code = "Unknown", Value = "Unknown!" },
                new Translate { Id = 85, LangId = 1, Code = "NewPassword", Value = "Yeni Parola:" },
                new Translate { Id = 86, LangId = 2, Code = "NewPassword", Value = "New Password:" },
                new Translate { Id = 87, LangId = 1, Code = "ChangePassword", Value = "Parola Değiştir" },
                new Translate { Id = 88, LangId = 2, Code = "ChangePassword", Value = "Change Password" },
                new Translate { Id = 89, LangId = 1, Code = "Save", Value = "Kaydet" },
                new Translate { Id = 90, LangId = 2, Code = "Save", Value = "Save" },
                new Translate { Id = 91, LangId = 1, Code = "GroupName", Value = "Grup Adı" },
                new Translate { Id = 92, LangId = 2, Code = "GroupName", Value = "Group Name" },
                new Translate { Id = 93, LangId = 1, Code = "FullName", Value = "Tam Adı" },
                new Translate { Id = 94, LangId = 2, Code = "FullName", Value = "Full Name" },
                new Translate { Id = 95, LangId = 1, Code = "Address", Value = "Adres" },
                new Translate { Id = 96, LangId = 2, Code = "Address", Value = "Address" },
                new Translate { Id = 97, LangId = 1, Code = "Notes", Value = "Notlar" },
                new Translate { Id = 98, LangId = 2, Code = "Notes", Value = "Notes" },
                new Translate { Id = 99, LangId = 1, Code = "ConfirmPassword", Value = "Parolayı Doğrula" },
                new Translate { Id = 100, LangId = 2, Code = "ConfirmPassword", Value = "Confirm Password" },
                new Translate { Id = 101, LangId = 1, Code = "Code", Value = "Kod" },
                new Translate { Id = 102, LangId = 2, Code = "Code", Value = "Code" },
                new Translate { Id = 103, LangId = 1, Code = "Alias", Value = "Görünen Ad" },
                new Translate { Id = 104, LangId = 2, Code = "Alias", Value = "Alias" },
                new Translate { Id = 105, LangId = 1, Code = "Description", Value = "Açıklama" },
                new Translate { Id = 106, LangId = 2, Code = "Description", Value = "Description" },
                new Translate { Id = 107, LangId = 1, Code = "Value", Value = "Değer" },
                new Translate { Id = 108, LangId = 2, Code = "Value", Value = "Value" },
                new Translate { Id = 109, LangId = 1, Code = "LangCode", Value = "Dil Kodu" },
                new Translate { Id = 110, LangId = 2, Code = "LangCode", Value = "Lang Code" },
                new Translate { Id = 111, LangId = 1, Code = "Name", Value = "Adı" },
                new Translate { Id = 112, LangId = 2, Code = "Name", Value = "Name" },
                new Translate { Id = 113, LangId = 1, Code = "MobilePhones", Value = "Cep Telefonu" },
                new Translate { Id = 114, LangId = 2, Code = "MobilePhones", Value = "Mobile Phone" },
                new Translate { Id = 115, LangId = 1, Code = "NoRecordsFound", Value = "Kayıt Bulunamadı" },
                new Translate { Id = 116, LangId = 2, Code = "NoRecordsFound", Value = "No Records Found" },
                new Translate { Id = 117, LangId = 1, Code = "Required", Value = "Bu alan zorunludur!" },
                new Translate { Id = 118, LangId = 2, Code = "Required", Value = "This field is required!" },
                new Translate { Id = 119, LangId = 1, Code = "Permissions", Value = "Permissions" },
                new Translate { Id = 120, LangId = 2, Code = "Permissions", Value = "İzinler" },
                new Translate { Id = 121, LangId = 1, Code = "GroupList", Value = "Grup Listesi" },
                new Translate { Id = 122, LangId = 2, Code = "GroupList", Value = "Group List" },
                new Translate { Id = 123, LangId = 1, Code = "GrupPermissions", Value = "Grup Yetkileri" },
                new Translate { Id = 124, LangId = 2, Code = "GrupPermissions", Value = "Grup Permissions" },
                new Translate { Id = 125, LangId = 1, Code = "Add", Value = "Ekle" },
                new Translate { Id = 126, LangId = 2, Code = "Add", Value = "Add" },
                new Translate { Id = 127, LangId = 1, Code = "UserList", Value = "Kullanıcı Listesi" },
                new Translate { Id = 128, LangId = 2, Code = "UserList", Value = "User List" },
                new Translate { Id = 129, LangId = 1, Code = "OperationClaimList", Value = "Yetki Listesi" },
                new Translate { Id = 130, LangId = 2, Code = "OperationClaimList", Value = "OperationClaim List" },
                new Translate { Id = 131, LangId = 1, Code = "LanguageList", Value = "Dil Listesi" },
                new Translate { Id = 132, LangId = 2, Code = "LanguageList", Value = "Language List" },
                new Translate { Id = 133, LangId = 1, Code = "TranslateList", Value = "Dil Çeviri Listesi" },
                new Translate { Id = 134, LangId = 2, Code = "TranslateList", Value = "Translate List" },
                new Translate { Id = 135, LangId = 1, Code = "LogList", Value = "İşlem Kütüğü" },
                new Translate { Id = 136, LangId = 2, Code = "LogList", Value = "LogList" },
                new Translate { Id = 137, LangId = 1, Code = "DeleteConfirm", Value = "Emin misiniz?" },
                new Translate { Id = 138, LangId = 2, Code = "DeleteConfirm", Value = "Are you sure?" }
            };
        }
    }
}