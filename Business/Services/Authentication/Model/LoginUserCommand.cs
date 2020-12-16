using Core.Utilities.Results;
using Core.Entities;
using MediatR;
using System.Text.RegularExpressions;

namespace Business.Services.Authentication.Model
{
    /// <summary>
    /// Farklı kullanıcı profilleri için sisteme giriş bilgilerini içerir.
    /// </summary>
    public class LoginUserCommand : IRequest<IDataResult<LoginUserResult>>
    {
        /// <summary>
        /// Provider'a göre değişen kullanıcı numarasıdır.
        /// 
        /// Person: TCKimlik
        /// Staff: Personel sicil numarası
        /// Agent: Çağrı merkeezi kullanıcı numarası
        /// </summary>
        public string ExternalUserId { get; set; }
        /// <summary>
        /// Numara seçildikten sonra bu alan doldurularak SMS doğrulama aşamasına geçilir.
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// Personel ve Agent girişlerinde kullanılır.
        /// </summary>
        public string Password { get; set; }
        public AuthenticationProviderType Provider { get; set; }
        public bool IsPhoneValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(MobilePhone))
                    return false;
                else
                {
                    PostProcess();
                    MobilePhone = Regex.Replace(MobilePhone, "[^0-9]", "");
                    return MobilePhone.StartsWith("05") && MobilePhone.Length == 11;
                }
            }
        }
        public long AsCitizenId() => long.Parse(ExternalUserId);
        /// <summary>
        /// Cep telefonu gibi alanları normalize eder.
        /// </summary>
        public void PostProcess()
        {
            MobilePhone = Regex.Replace(MobilePhone, "[^0-9]", "");
        }
    }
}
