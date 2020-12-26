
using Business.Handlers.Translates.Commands;
using FluentValidation;

namespace Business.Handlers.Translates.ValidationRules
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

    public class CreateTranslateValidator : AbstractValidator<CreateTranslateCommand>
    {
        public CreateTranslateValidator()
        {
            RuleFor(x => x.LangId).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();

        }
    }
    public class UpdateTranslateValidator : AbstractValidator<UpdateTranslateCommand>
    {
        public UpdateTranslateValidator()
        {
            RuleFor(x => x.LangId).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();

        }
    }
}