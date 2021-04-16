namespace Business.Services.Authentication
{
    using System.Threading.Tasks;
    using Business.Services.Authentication.Model;
    using Core.Utilities.Results;

    public interface IAuthenticationProvider
    {
        Task<LoginUserResult> Login(LoginUserCommand command);
        Task<IDataResult<DArchToken>> Verify(VerifyOtpCommand command);
    }
}