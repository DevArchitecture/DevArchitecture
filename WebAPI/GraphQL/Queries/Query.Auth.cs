using Business.Handlers.Authorizations.Queries;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<AccessToken>> Login(LoginUserQuery loginUserQuery)
        => GetResponseWithResult(await Mediator.Send(loginUserQuery));

    public async Task<DataResult<AccessToken>> ExternalLogin(ExternalLoginUserQuery externalLoginUserQuery)
        => GetResponseWithResult(await Mediator.Send(externalLoginUserQuery));

    public async Task<DataResult<AccessToken>> LoginWithRefreshToken(LoginWithRefreshTokenQuery loginWithRefreshTokenQuery)
        => GetResponseWithResult(await Mediator.Send(loginWithRefreshTokenQuery));

    public async Task<DataResult<bool>> Verification(VerifyCidQuery verifyCidQuery)
        => GetResponseWithResult(await Mediator.Send(verifyCidQuery));
}
