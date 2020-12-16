namespace Business.Services.Authentication.Model
{
    /// <summary>
    /// Login işlevinin dönüş verisidir.
    /// </summary>
    public class LoginUserResult
    {
        /// <summary>
        /// Login sorgu sonucudur.
        /// </summary>
        public LoginStatus Status { get; set; }
        /// <summary>
        /// Ek mesaj
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Sistemde bulunan kullanıcılar için kayıtlı tel numaraları listesi.
        /// </summary>
        public string[] MobilePhones { get; set; }

        public enum LoginStatus
        {
            UserNotFound,
            WrongCredentials,
            PhoneNumberRequired,
            ServiceError,
            Ok
        }
    }
}
