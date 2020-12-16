using System.Threading.Tasks;

namespace Business.Services.Authentication
{
    /// <summary>
    /// Giriş veri sağlayıcıları sisteme giriş yaparken bizi kodlanmış bir parametreyle çağırırlar.
    /// Bu interface o kodu belli bir url'ye sorarak bilgileri çözdürdüğümüz sağlayıcıların
    /// uyması gereken arayüzü tanımlar.
    /// </summary>
    public interface ILoginDataProvider
    {
        Task<LoginDataProviderResult> Verify(string accessToken);
    }

    public class LoginDataProviderResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ExternalUser ExternalUser { get; set; }
    }


    /// <summary>
    /// Sisteme agent olarak belli bir bağışçının işlemlerini yürütmek üzere
    /// login olacak kullanıcı ve ilgili bağışçının temel veri paketi.
    /// </summary>
    /// <remarks>
    /// Verilen GUID REST servisinden çekildiğinde alınması beklenen alanları içerir.
    /// </remarks>
    public class ExternalUser
    {
        /// <summary>
        /// Muhattapla o an için işlem yapan Dış kullanıcının kendi sisteminde tanımlı anahtarıdır.
        /// </summary>
        public string AgentUserId { get; set; }

        /// <summary>
        /// Başvuran Vatandaşlık No
        /// </summary>
        public long CitizenId { get; set; }

        /// <summary>
        /// Kişinin Eposta adresi.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Cep telefonu
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Adres
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>		
        public string Notes { get; set; }

        public void CleanRecord()
        {

            // Tel no 3 adet filan gelebiliyor. 1. yi al gerisini salla.
            if (!string.IsNullOrWhiteSpace(MobilePhone))
            {
                // 0 ile baslamiyorsa ekle
                if (!MobilePhone.StartsWith("0"))
                    MobilePhone = "0" + MobilePhone;
                MobilePhone = MobilePhone.Split('-')[0].Trim();
            }
        }
    }
}
