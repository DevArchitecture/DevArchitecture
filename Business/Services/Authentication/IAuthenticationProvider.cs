using Business.Services.Authentication.Model;
using Core.Utilities.Results;
using System.Threading.Tasks;

namespace Business.Services.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<LoginUserResult> Login(LoginUserCommand command);
        Task<IDataResult<SFwToken>> Verify(VerifyOtpCommand command);
    }
}