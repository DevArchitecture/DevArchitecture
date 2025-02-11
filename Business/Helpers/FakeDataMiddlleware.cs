using System.Threading.Tasks;
using Business.Fakes.Handlers.Languages;
using Business.Fakes.Handlers.Translates;
using Business.Handlers.Groups.Commands;
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

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AppName", Value = "Flutter DevArchitecture" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AppName", Value = "Flutter DevArchitecture" });



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
                LangId = 1,
                Code = "StringLengthMustBeGreaterThanThree",
                Value = "Lütfen En Az 3 Karakterden Oluşan Bir İfade Girin."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2,
                Code = "StringLengthMustBeGreaterThanThree",
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
                LangId = 1,
                Code = "AuthorizationsDenied",
                Value = "Yetkiniz olmayan bir alana girmeye çalıştığınız tespit edildi."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2,
                Code = "AuthorizationsDenied",
                Value = "It has been detected that you are trying to enter an area that you do not have authorization."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 1,
                Code = "UserNotFound",
                Value = "Kimlik Bilgileri Doğrulanamadı. Lütfen Yeni Kayıt Ekranını kullanın."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2,
                Code = "UserNotFound",
                Value = "Credentials Could Not Verify. Please use the New Registration Screen."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 1,
                Code = "PasswordError",
                Value = "Kimlik Bilgileri Doğrulanamadı, Kullanıcı adı ve/veya parola hatalı."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2,
                Code = "PasswordError",
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
                LangId = 2,
                Code = "NameAlreadyExist",
                Value = "The Object You Are Trying To Create Already Exists."
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 1,
                Code = "WrongCitizenId",
                Value = "Vatandaşlık No Sistemimizde Bulunamadı. Lütfen Yeni Kayıt Oluşturun!"
            });
            await mediator.Send(new CreateTranslateInternalCommand
            {
                LangId = 2,
                Code = "WrongCitizenId",
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
                LangId = 2,
                Code = "SendPassword",
                Value = "Your new password has been sent to your e-mail address."
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
                LangId = 2,
                Code = "TrueButCellPhone",
                Value = "The information is correct. Cell phone is required."
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



            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "CantBeEmpty", Value = "boş bırakılamaz!" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "CantBeEmpty", Value = "can not be empty!" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "InvalidEmail", Value = "Lütfen geçerli bir e-posta giriniz!" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "InvalidEmail", Value = "Please enter a valid email address!" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "InvalidPassword", Value = "Lütfen geçerli bir parola giriniz!" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "InvalidPassword", Value = "Please enter a valid password address!" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "Status", Value = "Durum" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "Status", Value = "Status" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "UpdateUser", Value = "Kullanıcı bilgilerini düzenle" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "UpdateUser", Value = "Update User" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "PageNotFound", Value = "Sayfa bulunamadı!" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "PageNotFound", Value = "Page Not Found!" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "Loading", Value = "Gerekli işlemler yapılıyor" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "Loading", Value = "Loading" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "HomePage", Value = "Anasayfa" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "HomePage", Value = "Home Page" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "ReturnHomePage", Value = "Anasayfaya Geri Dön" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "ReturnHomePage", Value = "Return Home Page" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "Categories", Value = "Kategoriler" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "Categories", Value = "Categories" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "Sells", Value = "Satışlar" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "Sells", Value = "Sells" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 1, Code = "Days", Value = "Günler" });
            await mediator.Send(new CreateTranslateInternalCommand
            { LangId = 2, Code = "Days", Value = "Days" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "HomePageTitle", Value = "Anasayfa" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "HomePageTitle", Value = "Home Page" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AdminPanelPageTitle", Value = "Yönetim" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AdminPanelPageTitle", Value = "Admin Panel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AdminPanelHomePageTitle", Value = "Admin Paneli" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AdminPanelHomePageTitle", Value = "Admin Panel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AdminPanelPageUserTitle", Value = "Kullanıcılar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AdminPanelPageUserTitle", Value = "Users" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AdminPanelPageGroupTitle", Value = "Gruplar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AdminPanelPageGroupTitle", Value = "Groups" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AdminPanelPageOperationClaimTitle", Value = "Operasyon Yetkileri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AdminPanelPageOperationClaimTitle", Value = "Operation Claims" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AdminPanelPageLanguageTitle", Value = "Diller" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AdminPanelPageLanguageTitle", Value = "Languages" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AdminPanelPageTranslateTitle", Value = "Dil Çevirileri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AdminPanelPageTranslateTitle", Value = "Language Translations" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AdminPanelPageLogTitle", Value = "Log Kayıtları" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AdminPanelPageLogTitle", Value = "Log Records" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AppPanelPageTitle", Value = "Uygulama" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AppPanelPageTitle", Value = "Application" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UtilitiesPageTitle", Value = "Utilities" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UtilitiesPageTitle", Value = "Utilities" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "BatteryStatusPageTitle", Value = "Batarya Durumu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "BatteryStatusPageTitle", Value = "Battery Status" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "BiometricAuthPageTitle", Value = "Biyometrik Kimlik Dogrulama" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "BiometricAuthPageTitle", Value = "Biometric Authentication" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DeviceInfoPageTitle", Value = "Cihaz Bilgileri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DeviceInfoPageTitle", Value = "Device Info" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DownloadPageTitle", Value = "İndirme İşlemleri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DownloadPageTitle", Value = "Download Operations" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ExcelDownloadPageTitle", Value = "Excel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ExcelDownloadPageTitle", Value = "Excel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PdfDownloadPageTitle", Value = "PDF" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PdfDownloadPageTitle", Value = "PDF" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CsvDownloadPageTitle", Value = "CSV" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CsvDownloadPageTitle", Value = "CSV" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "JsonDownloadPageTitle", Value = "JSON" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "JsonDownloadPageTitle", Value = "JSON" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "XmlDownloadPageTitle", Value = "XML" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "XmlDownloadPageTitle", Value = "XML" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ImageDownloadPageTitle", Value = "Resim" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ImageDownloadPageTitle", Value = "Image" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TxtDownloadPageTitle", Value = "TXT" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TxtDownloadPageTitle", Value = "TXT" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SharePageTitle", Value = "Paylaşım İşlemleri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SharePageTitle", Value = "Share Operations" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ExcelSharePageTitle", Value = "Excel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ExcelSharePageTitle", Value = "Excel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PdfSharePageTitle", Value = "PDF" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PdfSharePageTitle", Value = "PDF" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CsvSharePageTitle", Value = "CSV" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CsvSharePageTitle", Value = "CSV" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "JsonSharePageTitle", Value = "JSON" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "JsonSharePageTitle", Value = "JSON" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "XmlSharePageTitle", Value = "XML" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "XmlSharePageTitle", Value = "XML" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ImageSharePageTitle", Value = "Resim" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ImageSharePageTitle", Value = "Image" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TxtSharePageTitle", Value = "TXT" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TxtSharePageTitle", Value = "TXT" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "InternetConnectionPageTitle", Value = "İnternet Bağlantısı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "InternetConnectionPageTitle", Value = "Internet Connection" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LocalNotificationPageTitle", Value = "Yerel Bildirimler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LocalNotificationPageTitle", Value = "Local Notifications" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ScreenMessagePageTitle", Value = "Ekran Mesajı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ScreenMessagePageTitle", Value = "Screen Message" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LoggerPageTitle", Value = "Log İşlemleri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LoggerPageTitle", Value = "Logger Operations" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PermissionPageTitle", Value = "İzinler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PermissionPageTitle", Value = "Permissions" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "QrCodeScannerPageTitle", Value = "QR Kod Okuma" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "QrCodeScannerPageTitle", Value = "QR Code Scanner" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TemplatesPageTitle", Value = "Tema" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TemplatesPageTitle", Value = "Templates" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ColorPalettePageTitle", Value = "Renk Paleti" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ColorPalettePageTitle", Value = "Color Palette" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "WidgetsPageTitle", Value = "Widgetler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "WidgetsPageTitle", Value = "Widgets" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "InputExamplesPageTitle", Value = "Girdi Olayları" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "InputExamplesPageTitle", Value = "Input Examples" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "FormValidationErrorMessage", Value = "Lütfen eksik bilgileri doldurun ve tekrar deneyin." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "FormValidationErrorMessage", Value = "Please fill in the missing information and try again." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerDefaultHttpMessage", Value = "Veriler getiriliyor" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerDefaultHttpMessage", Value = "Data is being retrieved" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerDefaultCalculateMessage", Value = "Hesaplama yapılıyor" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerDefaultCalculateMessage", Value = "Calculation is being performed" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerSaveInfoMessage", Value = "Kayıt İşlemi Yapılıyor..." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerSaveInfoMessage", Value = "Save operation is being performed..." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerInternetConnectionSuccessMessage", Value = "İnternet Bağlantısı başarılı!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerInternetConnectionSuccessMessage", Value = "Internet connection successful!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerDefaultWarningMessage", Value = "Bir problem olabilir." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerDefaultWarningMessage", Value = "There might be a problem." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerInternetConnectionErrorMessage", Value = "İnternet Bağlantınızı kontrol ediniz!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerInternetConnectionErrorMessage", Value = "Please check your internet connection!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerDefaultErrorMessage", Value = "Bir hata oluştu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerDefaultErrorMessage", Value = "An error occurred" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CompanyNameInputFieldWidgetEmptyErrorMessage", Value = "Firma Adı Boş Geçilemez!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CompanyNameInputFieldWidgetEmptyErrorMessage", Value = "Company name cannot be empty!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerSuccessHttpMessage", Value = "Veriler hazır" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerSuccessHttpMessage", Value = "Data is ready" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerSuccessSaveMessage", Value = "Kayıt İşlemi Başarılı!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerSuccessSaveMessage", Value = "Save operation successful!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerSuccessCalculateMessage", Value = "Hesaplama Tamamlandı!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerSuccessCalculateMessage", Value = "Calculation completed!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "FuelInputErrorMessage", Value = "Yakıt Alım Miktarı Kapasiteyi Aşıyor!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "FuelInputErrorMessage", Value = "Fuel intake exceeds capacity!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "FuelSpendErrorMessage", Value = "Yakıt Harcama Geçerli Miktarı Aşıyor!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "FuelSpendErrorMessage", Value = "Fuel expenditure exceeds valid amount!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerAddSuccessMessage", Value = "Veri Eklendi..." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerAddSuccessMessage", Value = "Data added..." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CheckSuccessMessage", Value = "Veri kontrolü başarılı!, Sisteme gönderiliyor..." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CheckSuccessMessage", Value = "Data verification successful! Sending to the system..." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ChangeTheme", Value = "Temayı Değiştir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ChangeTheme", Value = "Change Theme" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Notifications", Value = "Bildirimler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Notifications", Value = "Notifications" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ComingSoon", Value = "Yakında Geliyor" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ComingSoon", Value = "Coming Soon" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Profile", Value = "Profil" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Profile", Value = "Profile" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LogOut", Value = "Çıkış Yap" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LogOut", Value = "Log Out" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LoginTitle", Value = "Giriş Yap" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LoginTitle", Value = "Login" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LoginButton", Value = "Giriş Yap" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LoginButton", Value = "Login" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "RegisterButton", Value = "Kayıt Ol" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "RegisterButton", Value = "Register" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "RegisterTitle", Value = "Kayıt Ol" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "RegisterTitle", Value = "Register" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UsernameHint", Value = "Kullanıcı Adı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UsernameHint", Value = "Username" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PasswordHint", Value = "Şifre" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PasswordHint", Value = "Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ConfirmPasswordHint", Value = "Şifreyi Onayla" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ConfirmPasswordHint", Value = "Confirm Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "RememberMeHint", Value = "Beni Hatırla" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "RememberMeHint", Value = "Remember Me" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ForgotPasswordHint", Value = "Şifrenizi mi Unuttunuz" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ForgotPasswordHint", Value = "Forgot Your Password?" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ForgotPasswordTitle", Value = "Şifrenizi mi Unuttunuz" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ForgotPasswordTitle", Value = "Forgot Your Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SendButton", Value = "Gönder" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SendButton", Value = "Send" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SendAgainButton", Value = "Yeniden Gönder" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SendAgainButton", Value = "Send Again" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ResendButton", Value = "Tekrar Gönder" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ResendButton", Value = "Resend" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ResendAgainButton", Value = "Tekrar Gönder" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ResendAgainButton", Value = "Resend Again" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ResetButton", Value = "Şifreyi Sıfırla" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ResetButton", Value = "Reset" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ResetPasswordTitle", Value = "Şifreyi Sıfırla" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ResetPasswordTitle", Value = "Reset Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ResetPasswordHint", Value = "Yeni Şifre" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ResetPasswordHint", Value = "New Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ResetPasswordAgainHint", Value = "Yeni Şifreyi Onayla" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ResetPasswordAgainHint", Value = "Confirm New Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ResetPasswordButton", Value = "Şifreyi Sıfırla" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ResetPasswordButton", Value = "Reset Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "NewInputButton", Value = "Yeni Kayıt Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "NewInputButton", Value = "Add New Entry" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SummaryTitle", Value = "Özet" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SummaryTitle", Value = "Summary" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DateFormat", Value = "dd.MM.yyyy" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DateFormat", Value = "dd.MM.yyyy" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TimeFormat", Value = "HH:mm" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TimeFormat", Value = "HH:mm" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DateTimeFormat", Value = "dd.MM.yyyy HH:mm" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DateTimeFormat", Value = "dd.MM.yyyy HH:mm" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DateHint", Value = "Tarih" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DateHint", Value = "Date" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "OkButton", Value = "Tamam" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "OkButton", Value = "Ok" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CancelButton", Value = "İptal" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CancelButton", Value = "Cancel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "YesButton", Value = "Evet" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "YesButton", Value = "Yes" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "NoButton", Value = "Hayır" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "NoButton", Value = "No" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DeleteButton", Value = "Sil" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DeleteButton", Value = "Delete" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "EditButton", Value = "Düzenle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "EditButton", Value = "Edit" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SaveButton", Value = "Kaydet" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SaveButton", Value = "Save" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "BackButton", Value = "Geri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "BackButton", Value = "Back" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AddButton", Value = "Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AddButton", Value = "Add" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "FilterButton", Value = "Filtrele" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "FilterButton", Value = "Filter" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ClearButton", Value = "Temizle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ClearButton", Value = "Clear" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SortButton", Value = "Sırala" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SortButton", Value = "Sort" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Id", Value = "Id" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Id", Value = "Id" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CustomerDefaultSuccessMessage", Value = "İşlem başarıyla tamamlandı!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CustomerDefaultSuccessMessage", Value = "Operation completed successfully!" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SelectOutputFileMessage", Value = "Lütfen dosya adı ve yolunu seçin." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SelectOutputFileMessage", Value = "Select the output file." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DownloadSuccessMessage", Value = "İndirme işlemi başarılı." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DownloadSuccessMessage", Value = "Download completed successfully." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ComingSoon", Value = "Çok Yakında" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ComingSoon", Value = "Coming Soon" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DownloadSuccessMessage", Value = "İndirme Başarılı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DownloadSuccessMessage", Value = "Download Successful" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ThisActionCannotBeUndone", Value = "Bu İşlem Geri Alınamaz" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ThisActionCannotBeUndone", Value = "This Action Cannot Be Undone" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "MinCharacter", Value = "Minimum Karakter" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "MinCharacter", Value = "Minimum Character" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "MaxCharacter", Value = "Maksimum Karakter" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "MaxCharacter", Value = "Maximum Character" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "InvalidPhone", Value = "Geçersiz Telefon Numarası" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "InvalidPhone", Value = "Invalid Phone Number" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "InvalidPassword", Value = "Geçersiz Şifre" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "InvalidPassword", Value = "Invalid Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Minimum", Value = "Minimum" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Minimum", Value = "Minimum" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Maximum", Value = "Maksimum" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Maximum", Value = "Maximum" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "InvalidNumber", Value = "Geçersiz Numara" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "InvalidNumber", Value = "Invalid Number" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Invalid", Value = "Geçersiz" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Invalid", Value = "Invalid" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "InvalidDate", Value = "Geçersiz Tarih" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "InvalidDate", Value = "Invalid Date" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DateCantBeEmpty", Value = "Tarih Boş Olamaz" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DateCantBeEmpty", Value = "Date Can't Be Empty" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupPermissionUpdate", Value = "Grup İzinlerini Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupPermissionUpdate", Value = "Update Group Permissions" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupUpdate", Value = "Grup Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupUpdate", Value = "Group Update" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PasswordChange", Value = "Şifre Değişikliği" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PasswordChange", Value = "Password Change" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PermissionChange", Value = "Yetki Değişikliği" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PermissionChange", Value = "Permission Change" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupChange", Value = "Grup Değişikliği" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupChange", Value = "Group Change" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "EditMessage", Value = "Düzenle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "EditMessage", Value = "Edit" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DeleteMessage", Value = "Sil" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DeleteMessage", Value = "Delete" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DownloadMessage", Value = "İndir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DownloadMessage", Value = "Download" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AddMessage", Value = "Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AddMessage", Value = "Add" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DetailedInformationMessage", Value = "Detaylı Bilgi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DetailedInformationMessage", Value = "Detailed Information" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LogOutButton", Value = "Çıkış Yap" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LogOutButton", Value = "Log Out" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ProfileButton", Value = "Profil" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ProfileButton", Value = "Profile" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "NotificationsButton", Value = "Bildirimler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "NotificationsButton", Value = "Notifications" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ChangeThemeButton", Value = "Temayı Değiştir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ChangeThemeButton", Value = "Change Theme" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Categories", Value = "Kategoriler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Categories", Value = "Categories" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Sales", Value = "Satışlar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Sales", Value = "Sales" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Name", Value = "İsim" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Name", Value = "Name" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Days", Value = "Günler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Days", Value = "Days" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Months", Value = "Aylar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Months", Value = "Months" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Years", Value = "Yıllar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Years", Value = "Years" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Hours", Value = "Saatler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Hours", Value = "Hours" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Minutes", Value = "Dakikalar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Minutes", Value = "Minutes" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Seconds", Value = "Saniyeler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Seconds", Value = "Seconds" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Today", Value = "Bugün" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Today", Value = "Today" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Yesterday", Value = "Dün" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Yesterday", Value = "Yesterday" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Week", Value = "Hafta" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Week", Value = "Week" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Month", Value = "Ay" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Month", Value = "Month" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Year", Value = "Yıl" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Year", Value = "Year" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Day", Value = "Gün" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Day", Value = "Day" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Hour", Value = "Saat" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Hour", Value = "Hour" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Minute", Value = "Dakika" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Minute", Value = "Minute" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Second", Value = "Saniye" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Second", Value = "Second" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DataTableTitle", Value = "Veri Tablosu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DataTableTitle", Value = "Data Table Title" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PdfDownloadTooltip", Value = "PDF İndir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PdfDownloadTooltip", Value = "Download PDF" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ExcelDownloadTooltip", Value = "Excel İndir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ExcelDownloadTooltip", Value = "Download Excel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CsvDownloadTooltip", Value = "CSV İndir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CsvDownloadTooltip", Value = "Download CSV" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ImageDownloadTooltip", Value = "Resim İndir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ImageDownloadTooltip", Value = "Download Image" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TxtDownloadTooltip", Value = "TXT İndir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TxtDownloadTooltip", Value = "Download TXT" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "JsonDownloadTooltip", Value = "JSON İndir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "JsonDownloadTooltip", Value = "Download JSON" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "XmlDownloadTooltip", Value = "XML İndir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "XmlDownloadTooltip", Value = "Download XML" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PdfShareTooltip", Value = "PDF Paylaş" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PdfShareTooltip", Value = "Share PDF" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ExcelShareTooltip", Value = "Excel Paylaş" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ExcelShareTooltip", Value = "Share Excel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CsvShareTooltip", Value = "CSV Paylaş" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CsvShareTooltip", Value = "Share CSV" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ImageShareTooltip", Value = "Resim Paylaş" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ImageShareTooltip", Value = "Share Image" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TxtShareTooltip", Value = "TXT Paylaş" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TxtShareTooltip", Value = "Share TXT" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "JsonShareTooltip", Value = "JSON Paylaş" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "JsonShareTooltip", Value = "Share JSON" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "XmlShareTooltip", Value = "XML Paylaş" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "XmlShareTooltip", Value = "Share XML" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ShareTitle", Value = "Paylaşım " });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ShareTitle", Value = "Share Title" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "NoData", Value = "Veri Yok" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "NoData", Value = "No Data" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "NoConnection", Value = "Bağlantı Yok" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "NoConnection", Value = "No Connection" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "NoDataFound", Value = "Veri Bulunamadı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "NoDataFound", Value = "No Data Found" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Error", Value = "Hata" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Error", Value = "Error" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Attention", Value = "Dikkat" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Attention", Value = "Attention" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Warning", Value = "Uyarı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Warning", Value = "Warning" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Success", Value = "Başarılı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Success", Value = "Success" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Information", Value = "Bilgi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Information", Value = "Information" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Ok", Value = "Tamam" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Ok", Value = "Ok" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Cancel", Value = "İptal" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Cancel", Value = "Cancel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Yes", Value = "Evet" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Yes", Value = "Yes" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "No", Value = "Hayır" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "No", Value = "No" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Email", Value = "E-posta" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Email", Value = "Email" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Password", Value = "Şifre" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Password", Value = "Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ThisActionCannotBeUndone", Value = "Bu işlem geri alınamaz" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ThisActionCannotBeUndone", Value = "This action cannot be undone" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "NoDataAvailable", Value = "Mevcut veri yok" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "NoDataAvailable", Value = "No Data Available" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "VehicleRegistrationNumber", Value = "Araç Kayıt Numarası" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "VehicleRegistrationNumber", Value = "Vehicle Registration Number" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "VehicleLicensePlate", Value = "Araç Plakası" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "VehicleLicensePlate", Value = "Vehicle License Plate" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PhoneNumber", Value = "Telefon Numarası" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PhoneNumber", Value = "Phone Number" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Search", Value = "Ara" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Search", Value = "Search" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AdminPanel", Value = "Yönetim Paneli" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AdminPanel", Value = "Admin Panel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupInfoHover", Value = "Gruplar burada listelenmektedir." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupInfoHover", Value = "The group is listed." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AtLeastOneSelection", Value = "En Az Bir Seçim Yapılmalıdır" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AtLeastOneSelection", Value = "At Least One Selection Is Required" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LanguageInfoHover", Value = "Diller burada listelenmektedir." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LanguageInfoHover", Value = "The languages are listed." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LogInfoHover", Value = "Log bilgileri burada listelenmektedir." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LogInfoHover", Value = "Log information is listed." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "OperationClaimInfoHover", Value = "Operasyon yetkileri burada listelenmekteidr." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "OperationClaimInfoHover", Value = "Operation claims are listed." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TranslateInfoHover", Value = "Dil çevirileri burada listelenmektedir." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TranslateInfoHover", Value = "The language translations are listed." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PasswordsDoNotMatch", Value = "Şifreler eşleşmiyor" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PasswordsDoNotMatch", Value = "Passwords do not match" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupClaims", Value = "Grup Talepleri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupClaims", Value = "Group Claims" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SelectGroupClaim", Value = "Grup Talebi Seçin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SelectGroupClaim", Value = "Select Group Claim" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupList", Value = "Grup Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupList", Value = "Group List" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupName", Value = "Grup Adı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupName", Value = "Group Name" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupNameHint", Value = "Grup Adı İpucu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupNameHint", Value = "Group Name Hint" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AddGroup", Value = "Grup Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AddGroup", Value = "Add Group" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateGroupClaims", Value = "Grup Taleplerini Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateGroupClaims", Value = "Update Group Claims" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateButton", Value = "Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateButton", Value = "Update" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateGroup", Value = "Grubu Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateGroup", Value = "Update Group" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateGroupUsers", Value = "Grup Kullanıcılarını Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateGroupUsers", Value = "Update Group Users" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LanguageList", Value = "Dil Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LanguageList", Value = "Language List" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Code", Value = "Kod" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Code", Value = "Code" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AddLanguage", Value = "Dil Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AddLanguage", Value = "Add Language" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateLanguage", Value = "Dili Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateLanguage", Value = "Update Language" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LogList", Value = "Log Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LogList", Value = "Log List" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Level", Value = "Seviye" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Level", Value = "Level" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ExceptionMessage", Value = "İstisna Mesajı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ExceptionMessage", Value = "Exception Message" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TimeStamp", Value = "Zaman Damgası" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TimeStamp", Value = "Time Stamp" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "User", Value = "Kullanıcı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "User", Value = "User" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Value", Value = "Değer" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Value", Value = "Value" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Type", Value = "Tür" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Type", Value = "Type" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateOperationClaim", Value = "Operasyon Talebini Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateOperationClaim", Value = "Update Operation Claim" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AliasHint", Value = "Takma Ad İpucu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AliasHint", Value = "Alias Hint" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Description", Value = "Açıklama" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Description", Value = "Description" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "OperationClaimList", Value = "Operasyon Talepleri Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "OperationClaimList", Value = "Operation Claim List" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Language", Value = "Dil" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Language", Value = "Language" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TranslateList", Value = "Çeviri Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TranslateList", Value = "Translate List" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CodeHint", Value = "Kod İpucu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CodeHint", Value = "Code Hint" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ValueHint", Value = "Değer İpucu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ValueHint", Value = "Value Hint" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AddTranslate", Value = "Çeviri Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AddTranslate", Value = "Add Translate" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateTranslate", Value = "Çeviriyi Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateTranslate", Value = "Update Translate" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UserClaims", Value = "Kullanıcı Talepleri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UserClaims", Value = "User Claims" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SelectUserClaims", Value = "Kullanıcı Taleplerini Seçin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SelectUserClaims", Value = "Select User Claims" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SelectUsers", Value = "Kullanıcıları Seçin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SelectUsers", Value = "Select Users" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Users", Value = "Kullanıcılar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Users", Value = "Users" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UserGroups", Value = "Kullanıcı Grupları" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UserGroups", Value = "User Groups" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SelectUserGroups", Value = "Kullanıcı Gruplarını Seçin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SelectUserGroups", Value = "Select User Groups" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AddUser", Value = "Kullanıcı Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AddUser", Value = "Add User" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "FullName", Value = "Tam Ad" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "FullName", Value = "Full Name" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "FullNameHint", Value = "Tam Ad İpucu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "FullNameHint", Value = "Full Name Hint" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Address", Value = "Adres" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Address", Value = "Address" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AddressHint", Value = "Adres İpucu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AddressHint", Value = "Address Hint" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Active", Value = "Aktif" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Active", Value = "Active" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Inactive", Value = "Pasif" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Inactive", Value = "Inactive" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Notes", Value = "Notlar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Notes", Value = "Notes" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "NotesHint", Value = "Notlar İpucu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "NotesHint", Value = "Notes Hint" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ChangePassword", Value = "Şifreyi Değiştir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ChangePassword", Value = "Change Password" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "NewPassword", Value = "Yeni Şifre" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "NewPassword", Value = "New Password" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ConfirmPassword", Value = "Şifreyi Onayla" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ConfirmPassword", Value = "Confirm Password" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateUserClaims", Value = "Kullanıcı Taleplerini Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateUserClaims", Value = "Update User Claims" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateUserGroups", Value = "Kullanıcı Gruplarını Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateUserGroups", Value = "Update User Groups" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UpdateUsers", Value = "Kullanıcıları Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UpdateUsers", Value = "Update Users" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UserList", Value = "Kullanıcı Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UserList", Value = "User List" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Status", Value = "Durum" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Status", Value = "Status" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "MobilePhones", Value = "Cep Telefonları" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "MobilePhones", Value = "Mobile Phones" });
            
            // Create default group
            await mediator.Send(new CreateGroupCommand
            {
                GroupName = "Default Group"
            });
        }
    }
}