using System.Text.RegularExpressions;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using MediatR;

namespace Business.Services.Authentication.Model
{
    /// <summary>
    /// It contains login information for different user profiles.
    /// </summary>
    public class LoginUserCommand : IRequest<IDataResult<LoginUserResult>>
    {
        /// <summary>
        /// It is the user number that changes according to the provider.
        ///
        /// Person: CitizenId
        /// Staff: Personnel registration number
        /// Agent: External staff registration number
        /// </summary>
        public string ExternalUserId { get; set; }

        /// <summary>
        /// After the number is selected, this field is filled and SMS verification phase is started.
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// It is used for personnel and external personal logins.
        /// </summary>
        public string Password { get; set; }

        public AuthenticationProviderType Provider { get; set; }

        public bool IsPhoneValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(MobilePhone))
                {
                    return false;
                }

                PostProcess();
                MobilePhone = Regex.Replace(MobilePhone, "[^0-9]", string.Empty);
                return MobilePhone.StartsWith("05") && MobilePhone.Length == 11;
            }
        }

        public long AsCitizenId() => long.Parse(ExternalUserId);

        /// <summary>
        /// Normalizes areas such as mobile phones.
        /// </summary>
        public void PostProcess()
        {
            MobilePhone = Regex.Replace(MobilePhone, "[^0-9]", string.Empty);
        }
    }
}