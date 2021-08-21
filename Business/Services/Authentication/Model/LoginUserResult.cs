namespace Business.Services.Authentication.Model
{
    /// <summary>
    /// It is the return data of the login function.
    /// </summary>
    public class LoginUserResult
    {
        public enum LoginStatus
        {
            UserNotFound,
            WrongCredentials,
            PhoneNumberRequired,
            ServiceError,
            Ok
        }

        /// <summary>
        /// Login query result.
        /// </summary>
        public LoginStatus Status { get; set; }

        /// <summary>
        /// Additional message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// List of registered phone numbers for users in the system.
        /// </summary>
        public string[] MobilePhones { get; set; }
    }
}