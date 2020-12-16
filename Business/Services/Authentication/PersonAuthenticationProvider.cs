
using Business.Services.Authentication.Model;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Business.Adapters.SmsService;

namespace Business.Services.Authentication
{
    /// <summary>
    /// SFw veritabanını kullanarak login olan provider dır.
    /// </summary>
    public class PersonAuthenticationProvider : AuthenticationProviderBase, IAuthenticationProvider
    {
        private readonly IUserRepository _users;

        private readonly ITokenHelper _tokenHelper;
        public AuthenticationProviderType ProviderType { get; }
        public PersonAuthenticationProvider(AuthenticationProviderType providerType, IUserRepository users, IMobileLoginRepository mobileLogins, ITokenHelper tokenHelper, ISmsService smsService)
                        : base(mobileLogins, smsService)
        {
            _users = users;
            ProviderType = providerType;
            _tokenHelper = tokenHelper;
        }

        public override async Task<LoginUserResult> Login(LoginUserCommand command)
        {
            var citizenId = command.AsCitizenId();
            var user = await _users.Query()
                            .Where(u => u.CitizenId == citizenId)
                            .FirstOrDefaultAsync();



            if (command.IsPhoneValid)
                return await PrepareOneTimePassword(AuthenticationProviderType.Person, user.MobilePhones, user.CitizenId.ToString());
            else
                return new LoginUserResult
                {
                    Message = "Bilgiler doğru. Cep telefonu gerekiyor.",
                    Status = LoginUserResult.LoginStatus.PhoneNumberRequired,
                    MobilePhones = new string[] { user.MobilePhones }
                };
        }

        public override async Task<SFwToken> CreateToken(VerifyOtpCommand command)
        {
            var citizenId = long.Parse(command.ExternalUserId);
            var user = await _users.GetAsync(u => u.CitizenId == citizenId);
            user.AuthenticationProviderType = ProviderType.ToString();

            var claims = _users.GetClaims(user.UserId);
            var accessToken = _tokenHelper.CreateToken<SFwToken>(user, claims);
            accessToken.Provider = ProviderType;
            return accessToken;
        }
    }
}
