using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Authorizations.Queries;
using Business.Services.Authentication.Model;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Authorization işlemlerini yapar
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Dependency injection constructor injection ile sağlanır.
        /// </summary>
        /// <param name="configuration"></param>
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Kullanıcı Giriş Metodu İşlemlerini Yapar.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>  
        [ProducesResponseType(typeof(LoginUserResult), 200)]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginModel)
        {
            var result = await Mediator.Send(loginModel);
            if (result.Success)
                return Ok(result);
            return Unauthorized(result.Message);
        }

        /// <summary>
        ///  Kullanıcı Kayıt Metodu İşlemlerini yapar.
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand createUser)
        {
            var result = await Mediator.Send(createUser);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
        ///<summary>
        ///Parolamı Unuttum.
        ///</summary>
        ///<remarks>tckimlikno</remarks>
        ///<return></return>
        ///<response code="200"></response>   
        [HttpPut("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand forgotPassword)
        {
            var result = await Mediator.Send(forgotPassword);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }


        /// <summary>
        /// Mobil Giriş.
        /// </summary>
        /// <param name="verifyCid"></param>
        /// <returns></returns>
        //[ProducesResponseType(typeof(SFwToken), 200)]
        [AllowAnonymous]
        [HttpPost("verify")]
        public async Task<IActionResult> Verification([FromBody] VerifyCidQuery verifyCid)
        {
            var result = await Mediator.Send(verifyCid);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }


        /// <summary>
        /// Sisteme giriş yapıldıktan sonra erişilebilen ve token gerektiren kaynak.
        /// </summary>
        /// <returns></returns>
        [HttpPost("test")]
        public IActionResult LoginTest()
        {
            var auth = Request.Headers["Authorization"];
            var token = new JwtHelper(_configuration).DecodeToken(auth);
            return Ok(token);
        }
    }
}
