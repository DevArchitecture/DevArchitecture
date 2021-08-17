using System.Threading.Tasks;
using Business.Services.Authentication.Model;
using Core.Utilities.Results;

namespace Business.Services.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<LoginUserResult> Login(LoginUserCommand command);
        Task<IDataResult<DArchToken>> Verify(VerifyOtpCommand command);
    }
}