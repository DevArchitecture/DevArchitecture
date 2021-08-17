using System.Threading.Tasks;
using Business.Fakes.Handlers.Languages;
using Business.Fakes.Handlers.Translates;
using Core.Utilities.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Helpers
{
    public static class FakeDataMiddlleware
    {
        public static async Task UseDbFakeDataCreator(this IApplicationBuilder app)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            await mediator.Send(new CreateLanguageInternalCommand { Code = "tr-TR", Name = "Türkçe" });
            await mediator.Send(new CreateLanguageInternalCommand { Code = "en-EN", Name = "English" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Login", Value = "Giriş" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Email", Value = "E-posta" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Password", Value = "Parola" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Update", Value = "Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Delete", Value = "Sil" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "UsersGroups", Value = "Kullanıcının Grupları" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "UsersClaims", Value = "Kullanıcının Yetkileri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Create", Value = "Yeni" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Users", Value = "Kullanıcılar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Groups", Value = "Gruplar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Login", Value = "Login" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Email", Value = "Email" });
            await mediator.Send(
                new CreateTranslateInternalCommand { LangId = 2, Code = "Password", Value = "Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Update", Value = "Update" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Delete", Value = "Delete" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "UsersGroups", Value = "User's Groups" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "UsersClaims", Value = "User's Claims" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Create", Value = "Create" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Users", Value = "Users" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Groups", Value = "Groups" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "OperationClaim", Value = "Operasyon Yetkileri" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "OperationClaim", Value = "Operation Claim" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Languages", Value = "Diller" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "Languages", Value = "Languages" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "TranslateWords", Value = "Dil Çevirileri" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "TranslateWords", Value = "Translate Words" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Management", Value = "Yönetim" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "Management", Value = "Management" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "AppMenu", Value = "Uygulama" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "AppMenu", Value = "App Menu" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Added", Value = "Başarıyla Eklendi." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "Added", Value = "Successfully Added." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Updated", Value = "Başarıyla Güncellendi." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "Updated", Value = "Successfully Updated." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Deleted", Value = "Başarıyla Silindi." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "Deleted", Value = "Successfully Deleted." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "OperationClaimExists", Value = "Bu operasyon izni zaten mevcut." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "OperationClaimExists", Value = "This operation permit already exists." });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 1, Code = "StringLengthMustBeGreaterThanThree",
                Value = "Lütfen En Az 3 Karakterden Oluşan Bir İfade Girin."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2, Code = "StringLengthMustBeGreaterThanThree",
                Value = "Please Enter A Phrase Of At Least 3 Characters."
            });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "CouldNotBeVerifyCid", Value = "Kimlik No Doğrulanamadı." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "CouldNotBeVerifyCid", Value = "Could not be verify Citizen Id" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "VerifyCid", Value = "Kimlik No Doğrulandı." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "VerifyCid", Value = "Verify Citizen Id" });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 1, Code = "AuthorizationsDenied",
                Value = "Yetkiniz olmayan bir alana girmeye çalıştığınız tespit edildi."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2, Code = "AuthorizationsDenied",
                Value = "It has been detected that you are trying to enter an area that you do not have authorization."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 1, Code = "UserNotFound",
                Value = "Kimlik Bilgileri Doğrulanamadı. Lütfen Yeni Kayıt Ekranını kullanın."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2, Code = "UserNotFound",
                Value = "Credentials Could Not Verify. Please use the New Registration Screen."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 1, Code = "PasswordError",
                Value = "Kimlik Bilgileri Doğrulanamadı, Kullanıcı adı ve/veya parola hatalı."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2, Code = "PasswordError",
                Value = "Credentials Failed to Authenticate, Username and / or password incorrect."
            });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "SuccessfulLogin", Value = "Sisteme giriş başarılı." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "SuccessfulLogin", Value = "Login to the system is successful." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "SendMobileCode", Value = "Lütfen Size SMS Olarak Gönderilen Kodu Girin!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "SendMobileCode", Value = "Please Enter The Code Sent To You By SMS!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "NameAlreadyExist", Value = "Oluşturmaya Çalıştığınız Nesne Zaten Var." });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2, Code = "NameAlreadyExist", Value = "The Object You Are Trying To Create Already Exists."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 1, Code = "WrongCitizenId",
                Value = "Vatandaşlık No Sistemimizde Bulunamadı. Lütfen Yeni Kayıt Oluşturun!"
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2, Code = "WrongCitizenId",
                Value = "Citizenship Number Not Found In Our System. Please Create New Registration!"
            });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "CitizenNumber", Value = "Vatandaşlık No" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "CitizenNumber", Value = "Citizenship Number" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "PasswordEmpty", Value = "Parola boş olamaz!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "PasswordEmpty", Value = "Password can not be empty!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "PasswordLength", Value = "Minimum 8 Karakter Uzunluğunda Olmalıdır!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "PasswordLength", Value = "Must be at least 8 characters long! " });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "PasswordUppercaseLetter", Value = "En Az 1 Büyük Harf İçermelidir!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "PasswordUppercaseLetter", Value = "Must Contain At Least 1 Capital Letter!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "PasswordLowercaseLetter", Value = "En Az 1 Küçük Harf İçermelidir!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "PasswordLowercaseLetter", Value = "Must Contain At Least 1 Lowercase Letter!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "PasswordDigit", Value = "En Az 1 Rakam İçermelidir!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "PasswordDigit", Value = "It Must Contain At Least 1 Digit!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "PasswordSpecialCharacter", Value = "En Az 1 Simge İçermelidir!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "PasswordSpecialCharacter", Value = "Must Contain At Least 1 Symbol!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "SendPassword", Value = "Yeni Parolanız E-Posta Adresinize Gönderildi." });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2, Code = "SendPassword", Value = "Your new password has been sent to your e-mail address."
            });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "InvalidCode", Value = "Geçersiz Bir Kod Girdiniz!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "InvalidCode", Value = "You Entered An Invalid Code!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "SmsServiceNotFound", Value = "SMS Servisine Ulaşılamıyor." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "SmsServiceNotFound", Value = "Unable to Reach SMS Service." });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "TrueButCellPhone", Value = "Bilgiler doğru. Cep telefonu gerekiyor." });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2, Code = "TrueButCellPhone", Value = "The information is correct. Cell phone is required."
            });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "TokenProviderException", Value = "Token Provider boş olamaz!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "TokenProviderException", Value = "Token Provider cannot be empty!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Unknown", Value = "Bilinmiyor!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "Unknown", Value = "Unknown!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "ChangePassword", Value = "Parola Değiştir" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "ChangePassword", Value = "Change Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Save", Value = "Kaydet" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Save", Value = "Save" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "GroupName", Value = "Grup Adı" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "GroupName", Value = "Group Name" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "FullName", Value = "Tam Adı" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "FullName", Value = "Full Name" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Address", Value = "Adres" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Address", Value = "Address" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Notes", Value = "Notlar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Notes", Value = "Notes" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "ConfirmPassword", Value = "Parola Doğrula" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "ConfirmPassword", Value = "Confirm Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Code", Value = "Kod" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Code", Value = "Code" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Alias", Value = "Görünen Ad" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Alias", Value = "Alias" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Description", Value = "Açıklama" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "Description", Value = "Description" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Value", Value = "Değer" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Value", Value = "Value" });
            await mediator.Send(
                new CreateTranslateInternalCommand { LangId = 1, Code = "LangCode", Value = "Dil Kodu" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "LangCode", Value = "Lang Code" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Name", Value = "Adı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Name", Value = "Name" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "MobilePhones", Value = "Cep Telefonu" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "MobilePhones", Value = "Mobile Phone" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "NoRecordsFound", Value = "Kayıt Bulunamadı" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "NoRecordsFound", Value = "No Records Found" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Required", Value = "Bu alan zorunludur!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "Required", Value = "This field is required!" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "Permissions", Value = "Permissions" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "Permissions", Value = "İzinler" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "GroupList", Value = "Grup Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "GroupList", Value = "Group List" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "GrupPermissions", Value = "Grup Yetkileri" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "GrupPermissions", Value = "Grup Permissions" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Add", Value = "Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Add", Value = "Add" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "UserList", Value = "Kullanıcı Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "UserList", Value = "User List" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "OperationClaimList", Value = "Yetki Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "OperationClaimList", Value = "OperationClaim List" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "LanguageList", Value = "Dil Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "LanguageList", Value = "Language List" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "TranslateList", Value = "Dil Çeviri Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "TranslateList", Value = "Translate List" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "LogList", Value = "İşlem Kütüğü" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LogList", Value = "LogList" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 1, Code = "DeleteConfirm", Value = "Emin misiniz?" });
            await mediator.Send(new CreateTranslateInternalCommand
                { LangId = 2, Code = "DeleteConfirm", Value = "Are you sure?" });
        }
    }
}