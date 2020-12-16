using Business.Helpers;
using Business.Services.Authentication.Model;
using Core.Entities;
using FluentValidation;

namespace Business.Handlers.Authorizations
{
    /// <summary>
    /// Bu sınıf FluentValidation kütüphanesini kullanır. 
    /// AbstractValidator içine  Handlerlarda bulunan komut yada Query nesnelerini nesnelerini alır
    /// işi validasyon yapmaktır. 
    /// iş kuralları buraya yazılmak gerçekten doğrulama gerektiren operasyonlarla birlikte kullanılır.
    /// her komut yada sorgu nesnesi için ayrı ayrı yazılabildiğinden 
    /// CRUD operasyonların herbiri için ayrı validasyonlar yapabilme yeteneği vardır.
    /// code syntaxı aşağıdaki gibidir. 
    /// Aspect olarak Hanler metodlarının üzerinde kullanılır.
    /// </summary>

    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(m => m.Password).NotEmpty()
              .When((i) => i.Provider != AuthenticationProviderType.Person);
            RuleFor(m => m.ExternalUserId).NotEmpty().Must((instance, value) =>
            {
                switch (instance.Provider)
                {
                    case AuthenticationProviderType.Person:
                        return value.IsCidValid();
                    case AuthenticationProviderType.Staff:
                        return true;
                    case AuthenticationProviderType.Agent:
                        break;
                    default:
                        break;
                }
                return false;
            })
            .WithMessage("Geçerli bir Kullanıcı Kodu giriniz!!")
            .OverridePropertyName("Tc Kimlik");
        }
    }

}
