using Business.Fakes.Handlers.Groups;
using Business.Fakes.Handlers.Languages;
using Business.Fakes.Handlers.Translates;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Helpers;

public static class FakeDataMiddlleware
{
    public static async Task UseDbFakeDataCreator(this IApplicationBuilder app)
    {
        var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

        await mediator.Send(new CreateLanguageInternalCommand { Code = "tr-TR", Name = "Türkçe" });
        await mediator.Send(new CreateLanguageInternalCommand { Code = "en-EN", Name = "English" });

        await mediator.Send(new CreateTranslatesInternalCommand
        {
            Translates = new Translate[] {
                new Translate
                { LangId = 1, Code = "Login", Value = "Giriş"},
                new Translate
                { LangId = 1, Code = "Email", Value = "E-posta" },
                new Translate
                { LangId = 1, Code = "Password", Value = "Parola" },
                new Translate 
                { LangId = 1, Code = "Update", Value = "Güncelle" },
                new Translate 
                { LangId = 1, Code = "Delete", Value = "Sil" },
                new Translate
                { LangId = 1, Code = "UsersGroups", Value = "Kullanıcının Grupları" },
                new Translate
                { LangId = 1, Code = "UsersClaims", Value = "Kullanıcının Yetkileri" },
                new Translate 
                { LangId = 1, Code = "Create", Value = "Yeni" },
                new Translate
                { LangId = 1, Code = "Users", Value = "Kullanıcılar" },
                new Translate 
                { LangId = 1, Code = "Groups", Value = "Gruplar" },
                new Translate 
                { LangId = 2, Code = "Login", Value = "Login" },
                new Translate 
                { LangId = 2, Code = "Email", Value = "Email" },
                new Translate 
                { LangId = 2, Code = "Password", Value = "Password" },
                new Translate 
                { LangId = 2, Code = "Update", Value = "Update" },
                new Translate 
                { LangId = 2, Code = "Delete", Value = "Delete" },
                new Translate
                { LangId = 2, Code = "UsersGroups", Value = "User's Groups" },
                new Translate
                { LangId = 2, Code = "UsersClaims", Value = "User's Claims" },
                new Translate 
                { LangId = 2, Code = "Create", Value = "Create" },
                new Translate 
                { LangId = 2, Code = "Users", Value = "Users" },
                new Translate 
                { LangId = 2, Code = "Groups", Value = "Groups" },
                new Translate
                { LangId = 1, Code = "OperationClaim", Value = "Operasyon Yetkileri" },
                new Translate
                { LangId = 2, Code = "OperationClaim", Value = "Operation Claim" },
                new Translate
                { LangId = 1, Code = "Languages", Value = "Diller" },
                new Translate
                { LangId = 2, Code = "Languages", Value = "Languages" },
                new Translate
                { LangId = 1, Code = "TranslateWords", Value = "Dil Çevirileri" },
                new Translate
                { LangId = 2, Code = "TranslateWords", Value = "Translate Words" },
                new Translate
                { LangId = 1, Code = "Management", Value = "Yönetim" },
                new Translate
                { LangId = 2, Code = "Management", Value = "Management" },
                new Translate
                { LangId = 1, Code = "AppMenu", Value = "Uygulama" },
                new Translate
                { LangId = 2, Code = "AppMenu", Value = "App Menu" },
                new Translate
                { LangId = 1, Code = "Added", Value = "Başarıyla Eklendi." },
                new Translate
                { LangId = 2, Code = "Added", Value = "Successfully Added." },
                new Translate
                { LangId = 1, Code = "Updated", Value = "Başarıyla Güncellendi." },
                new Translate
                { LangId = 2, Code = "Updated", Value = "Successfully Updated." },
                new Translate
                { LangId = 1, Code = "Deleted", Value = "Başarıyla Silindi." },
                new Translate
                { LangId = 2, Code = "Deleted", Value = "Successfully Deleted." },
                new Translate
                { LangId = 1, Code = "OperationClaimExists", Value = "Bu operasyon izni zaten mevcut." },
                new Translate
                { LangId = 2, Code = "OperationClaimExists", Value = "This operation permit already exists." },
                new Translate
                {
                    LangId = 1,
                    Code = "StringLengthMustBeGreaterThanThree",
                    Value = "Lütfen En Az 3 Karakterden Oluşan Bir İfade Girin."
                },
                new Translate
                {
                    LangId = 2,
                    Code = "StringLengthMustBeGreaterThanThree",
                    Value = "Please Enter A Phrase Of At Least 3 Characters."
                },
                new Translate
                { LangId = 1, Code = "CouldNotBeVerifyCid", Value = "Kimlik No Doğrulanamadı." },
                new Translate
                { LangId = 2, Code = "CouldNotBeVerifyCid", Value = "Could not be verify Citizen Id" },
                new Translate
                { LangId = 1, Code = "VerifyCid", Value = "Kimlik No Doğrulandı." },
                new Translate
                { LangId = 2, Code = "VerifyCid", Value = "Verify Citizen Id" },
                new Translate
                {
                    LangId = 1,
                    Code = "AuthorizationsDenied",
                    Value = "Yetkiniz olmayan bir alana girmeye çalıştığınız tespit edildi."
                },
                new Translate
                {
                    LangId = 2,
                    Code = "AuthorizationsDenied",
                    Value = "It has been detected that you are trying to enter an area that you do not have authorization."
                },
                new Translate
                {
                    LangId = 1,
                    Code = "UserNotFound",
                    Value = "Kimlik Bilgileri Doğrulanamadı. Lütfen Yeni Kayıt Ekranını kullanın."
                },
                new Translate
                {
                    LangId = 2,
                    Code = "UserNotFound",
                    Value = "Credentials Could Not Verify. Please use the New Registration Screen."
                },
                new Translate
                {
                    LangId = 1,
                    Code = "PasswordError",
                    Value = "Kimlik Bilgileri Doğrulanamadı, Kullanıcı adı ve/veya parola hatalı."
                },
                new Translate
                {
                    LangId = 2,
                    Code = "PasswordError",
                    Value = "Credentials Failed to Authenticate, Username and / or password incorrect."
                },
                new Translate
                { LangId = 1, Code = "SuccessfulLogin", Value = "Sisteme giriş başarılı." },
                new Translate
                { LangId = 2, Code = "SuccessfulLogin", Value = "Login to the system is successful." },
                new Translate
                { LangId = 1, Code = "SendMobileCode", Value = "Lütfen Size SMS Olarak Gönderilen Kodu Girin!" },
                new Translate
                { LangId = 2, Code = "SendMobileCode", Value = "Please Enter The Code Sent To You By SMS!" },
                new Translate
                { LangId = 1, Code = "NameAlreadyExist", Value = "Oluşturmaya Çalıştığınız Nesne Zaten Var." },
                new Translate
                {
                    LangId = 2,
                    Code = "NameAlreadyExist",
                    Value = "The Object You Are Trying To Create Already Exists."
                },
                new Translate
                {
                    LangId = 1,
                    Code = "WrongCitizenId",
                    Value = "Vatandaşlık No Sistemimizde Bulunamadı. Lütfen Yeni Kayıt Oluşturun!"
                },
                new Translate
                {
                    LangId = 2,
                    Code = "WrongCitizenId",
                    Value = "Citizenship Number Not Found In Our System. Please Create New Registration!"
                },
                new Translate
                { LangId = 1, Code = "CitizenNumber", Value = "Vatandaşlık No" },
                new Translate
                { LangId = 2, Code = "CitizenNumber", Value = "Citizenship Number" },
                new Translate
                { LangId = 1, Code = "PasswordEmpty", Value = "Parola boş olamaz!" },
                new Translate
                { LangId = 2, Code = "PasswordEmpty", Value = "Password can not be empty!" },
                new Translate
                { LangId = 1, Code = "PasswordLength", Value = "Minimum 8 Karakter Uzunluğunda Olmalıdır!" },
                new Translate
                { LangId = 2, Code = "PasswordLength", Value = "Must be at least 8 characters long! " },
                new Translate
                { LangId = 1, Code = "PasswordUppercaseLetter", Value = "En Az 1 Büyük Harf İçermelidir!" },
                new Translate
                { LangId = 2, Code = "PasswordUppercaseLetter", Value = "Must Contain At Least 1 Capital Letter!" },
                new Translate
                { LangId = 1, Code = "PasswordLowercaseLetter", Value = "En Az 1 Küçük Harf İçermelidir!" },
                new Translate
                { LangId = 2, Code = "PasswordLowercaseLetter", Value = "Must Contain At Least 1 Lowercase Letter!" },
                new Translate
                { LangId = 1, Code = "PasswordDigit", Value = "En Az 1 Rakam İçermelidir!" },
                new Translate
                { LangId = 2, Code = "PasswordDigit", Value = "It Must Contain At Least 1 Digit!" },
                new Translate
                { LangId = 1, Code = "PasswordSpecialCharacter", Value = "En Az 1 Simge İçermelidir!" },
                new Translate
                { LangId = 2, Code = "PasswordSpecialCharacter", Value = "Must Contain At Least 1 Symbol!" },
                new Translate
                { LangId = 1, Code = "SendPassword", Value = "Yeni Parolanız E-Posta Adresinize Gönderildi." },
                new Translate
                {
                    LangId = 2,
                    Code = "SendPassword",
                    Value = "Your new password has been sent to your e-mail address."
                },
                new Translate
                { LangId = 1, Code = "InvalidCode", Value = "Geçersiz Bir Kod Girdiniz!" },
                new Translate
                { LangId = 2, Code = "InvalidCode", Value = "You Entered An Invalid Code!" },
                new Translate
                { LangId = 1, Code = "SmsServiceNotFound", Value = "SMS Servisine Ulaşılamıyor." },
                new Translate
                { LangId = 2, Code = "SmsServiceNotFound", Value = "Unable to Reach SMS Service." },
                new Translate
                { LangId = 1, Code = "TrueButCellPhone", Value = "Bilgiler doğru. Cep telefonu gerekiyor." },
                new Translate
                {
                    LangId = 2,
                    Code = "TrueButCellPhone",
                    Value = "The information is correct. Cell phone is required."
                },
                new Translate
                { LangId = 1, Code = "TokenProviderException", Value = "Token Provider boş olamaz!" },
                new Translate
                { LangId = 2, Code = "TokenProviderException", Value = "Token Provider cannot be empty!" },
                new Translate
                { LangId = 1, Code = "Unknown", Value = "Bilinmiyor!" },
                new Translate
                { LangId = 2, Code = "Unknown", Value = "Unknown!" },
                new Translate
                { LangId = 1, Code = "ChangePassword", Value = "Parola Değiştir" },
                new Translate
                { LangId = 2, Code = "ChangePassword", Value = "Change Password" },
                new Translate 
                { LangId = 1, Code = "Save", Value = "Kaydet" },
                new Translate 
                { LangId = 2, Code = "Save", Value = "Save" },
                new Translate
                { LangId = 1, Code = "GroupName", Value = "Grup Adı" },
                new Translate
                { LangId = 2, Code = "GroupName", Value = "Group Name" },
                new Translate
                { LangId = 1, Code = "FullName", Value = "Tam Adı" },
                new Translate
                { LangId = 2, Code = "FullName", Value = "Full Name" },
                new Translate 
                { LangId = 1, Code = "Address", Value = "Adres" },
                new Translate 
                { LangId = 2, Code = "Address", Value = "Address" },
                new Translate 
                { LangId = 1, Code = "Notes", Value = "Notlar" },
                new Translate 
                { LangId = 2, Code = "Notes", Value = "Notes" },
                new Translate
                { LangId = 1, Code = "ConfirmPassword", Value = "Parola Doğrula" },
                new Translate
                { LangId = 2, Code = "ConfirmPassword", Value = "Confirm Password" },
                new Translate 
                { LangId = 1, Code = "Code", Value = "Kod" },
                new Translate 
                { LangId = 2, Code = "Code", Value = "Code" },
                new Translate
                { LangId = 1, Code = "Alias", Value = "Görünen Ad" },
                new Translate 
                { LangId = 2, Code = "Alias", Value = "Alias" },
                new Translate
                { LangId = 1, Code = "Description", Value = "Açıklama" },
                new Translate
                { LangId = 2, Code = "Description", Value = "Description" },
                new Translate 
                { LangId = 1, Code = "Value", Value = "Değer" },
                new Translate 
                { LangId = 2, Code = "Value", Value = "Value" },
                new Translate 
                { LangId = 1, Code = "LangCode", Value = "Dil Kodu" },
                new Translate
                { LangId = 2, Code = "LangCode", Value = "Lang Code" },
                new Translate 
                { LangId = 1, Code = "Name", Value = "Adı" },
                new Translate 
                { LangId = 2, Code = "Name", Value = "Name" },
                new Translate
                { LangId = 1, Code = "MobilePhones", Value = "Cep Telefonu" },
                new Translate
                { LangId = 2, Code = "MobilePhones", Value = "Mobile Phone" },
                new Translate
                { LangId = 1, Code = "NoRecordsFound", Value = "Kayıt Bulunamadı" },
                new Translate
                { LangId = 2, Code = "NoRecordsFound", Value = "No Records Found" },
                new Translate
                { LangId = 1, Code = "Required", Value = "Bu alan zorunludur!" },
                new Translate
                { LangId = 2, Code = "Required", Value = "This field is required!" },
                new Translate
                { LangId = 1, Code = "Permissions", Value = "Permissions" },
                new Translate
                { LangId = 2, Code = "Permissions", Value = "İzinler" },
                new Translate
                { LangId = 1, Code = "GroupList", Value = "Grup Listesi" },
                new Translate
                { LangId = 2, Code = "GroupList", Value = "Group List" },
                new Translate
                { LangId = 1, Code = "GrupPermissions", Value = "Grup Yetkileri" },
                new Translate
                { LangId = 2, Code = "GrupPermissions", Value = "Grup Permissions" },
                new Translate 
                { LangId = 1, Code = "Add", Value = "Ekle" },
                new Translate 
                { LangId = 2, Code = "Add", Value = "Add" },
                new Translate
                { LangId = 1, Code = "UserList", Value = "Kullanıcı Listesi" },
                new Translate
                { LangId = 2, Code = "UserList", Value = "User List" },
                new Translate
                { LangId = 1, Code = "OperationClaimList", Value = "Yetki Listesi" },
                new Translate
                { LangId = 2, Code = "OperationClaimList", Value = "OperationClaim List" },
                new Translate
                { LangId = 1, Code = "LanguageList", Value = "Dil Listesi" },
                new Translate
                { LangId = 2, Code = "LanguageList", Value = "Language List" },
                new Translate
                { LangId = 1, Code = "TranslateList", Value = "Dil Çeviri Listesi" },
                new Translate
                { LangId = 2, Code = "TranslateList", Value = "Translate List" },
                new Translate
                { LangId = 1, Code = "LogList", Value = "İşlem Kütüğü" },
                new Translate 
                { LangId = 2, Code = "LogList", Value = "LogList" },
                new Translate
                { LangId = 1, Code = "DeleteConfirm", Value = "Emin misiniz?" },
                new Translate
                { LangId = 2, Code = "DeleteConfirm", Value = "Are you sure?" },
                new Translate
                { LangId = 1, Code = "GoogleLogin", Value = "Google ile giriş yap" },
                new Translate
                { LangId = 2, Code = "GoogleLogin", Value = "Sign in with Google" },
                new Translate
                { LangId = 1, Code = "FacebookLogin", Value = "Facebook ile giriş yap" },
                new Translate
                { LangId = 2, Code = "FacebookLogin", Value = "Sign in with Facebook" }
        }
        });
    }
}
