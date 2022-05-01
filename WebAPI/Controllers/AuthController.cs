using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Authorizations.Queries;
using Business.Handlers.Users.Commands;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// Make it Authorization operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseApiController
{
    /// <summary>
    /// Dependency injection is provided by constructor injection.
    /// </summary>
    public AuthController()
    {
    }

    /// <summary>
    /// Make it User Login operations
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AccessToken>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery loginModel)
    {
        var result = await Mediator.Send(loginModel);
        return result.Success ? Ok(result) : Unauthorized(result.Message);
    }

    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AccessToken>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [HttpPost("refreshToken")]
    public async Task<IActionResult> LoginWithRefreshToken([FromBody] LoginWithRefreshTokenQuery command)
    {
        var result = await Mediator.Send(command);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    ///  Make it User Register operations
    /// </summary>
    /// <param name="createUser"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand createUser) 
        => GetResponseOnlyResult(await Mediator.Send(createUser));

    /// <summary>
    /// Make it Forgot Password operations
    /// </summary>
    /// <remarks>tckimlikno</remarks>
    /// <return></return>
    /// <response code="200"></response>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    [HttpPut("forgotpassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand forgotPassword) 
        => GetResponseOnlyResult(await Mediator.Send(forgotPassword));

    /// <summary>
    /// Make it Change Password operation
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut("changeuserpassword")]
    public async Task<IActionResult> ChangeUserPassword([FromBody] UserChangePasswordCommand command) 
        => GetResponseOnlyResultMessage(await Mediator.Send(command));

    /// <summary>
    /// Mobile Login
    /// </summary>
    /// <param name="verifyCid"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost("verify")]
    public async Task<IActionResult> Verification([FromBody] VerifyCidQuery verifyCid) 
        => GetResponseOnlyResultMessage(await Mediator.Send(verifyCid));

    /// <summary>
    /// Token decode test
    /// </summary>
    /// <returns></returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [HttpPost("test")]
    public IActionResult LoginTest()
    {
        var auth = Request.Headers["Authorization"];
        var token = JwtHelper.DecodeToken(auth);

        return Ok(token);
    }
}
