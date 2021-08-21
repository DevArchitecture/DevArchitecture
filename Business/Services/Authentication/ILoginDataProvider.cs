using System.Threading.Tasks;

namespace Business.Services.Authentication
{
    /// <summary>
    /// Input data providers call us with a coded parameter when logging into the system.
    /// This interface defines the interface that the providers whose information is decoded by asking that code to a certain url.
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
    /// Outsource personnel information
    /// </summary>
    /// <remarks>
    /// Contains the fields expected to be received when the given GUID is withdrawn from the REST service.
    /// </remarks>
    public class ExternalUser
    {
        /// <summary>
        /// Outsource personnel Id
        /// </summary>
        public string AgentUserId { get; set; }

        /// <summary>
        /// Applicant Citizenship Number
        /// </summary>
        public long CitizenId { get; set; }

        /// <summary>
        /// Applicant Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Applicant Mobile Phone
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Applicant Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }

        public void CleanRecord()
        {
            if (string.IsNullOrWhiteSpace(MobilePhone))
            {
                return;
            }

            if (!MobilePhone.StartsWith("0"))
            {
                MobilePhone = "0" + MobilePhone;
            }

            MobilePhone = MobilePhone.Split('-')[0].Trim();
        }
    }
}