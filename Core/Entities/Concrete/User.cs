using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    /// <summary>
    /// Bağışçı ya da potansiyel bağışçı nesnesidir.
    /// </summary>
    public class User : IEntity
    {
        /// <summary>
        /// Kullanıcının sistem içindeki anahtarıdır.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Vatandaşlık no
        /// </summary>
        public long CitizenId { get; set; }
        /// <summary>
        /// Kullanıcının tam adıdır.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Eposta adresi.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Cep telefonu
        /// </summary>
        public string MobilePhones { get; set; }
        /// <summary>
        /// Durum.
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Doğum tarihi.
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Cinsiyet
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// Kayıt tarihi
        /// </summary>
        public DateTime RecordDate { get; set; }
        /// <summary>
        /// Adres
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Notlar
        /// </summary>		
        public string Notes { get; set; }
        /// <summary>
        /// Bağlantı bilgilerinin son güncellenme tarihi.
        /// </summary>
        public DateTime UpdateContactDate { get; set; }

        /// <summary>
        /// Bu token encode ederken gerekiyor. Db'de yok. Varsayılanı Person.
        /// </summary>
        [NotMapped]
        public string AuthenticationProviderType { get; set; } = "Person";


        /// <summary>
        /// Şifre ek bilgisi.
        /// </summary>
        public byte[] PasswordSalt { get; set; }
        /// <summary>
        /// Şifrenin çözülemeyecek şekilde kodlanmış hali.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        public User()
        {
            UpdateContactDate = RecordDate = DateTime.Now;
            Status = true;
        }

        public bool UpdateMobilePhone(string mobilePhone)
        {
            if (mobilePhone != MobilePhones)
            {
                MobilePhones = mobilePhone;
                return true;
            }
            else
                return false;
        }

    }
}
