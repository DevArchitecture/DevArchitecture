using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Users.Commands;
using Core.Utilities.Results;

namespace WebAPI.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Result> Register(RegisterUserCommand registerUserCommand)
        => GetResponse(await Mediator.Send(registerUserCommand));

    public async Task<Result> ForgotPassword(ForgotPasswordCommand forgotPasswordCommand)
        => GetResponse(await Mediator.Send(forgotPasswordCommand));

    public async Task<Result> ChangeUserPassword(UserChangePasswordCommand userChangePasswordCommand)
        => GetResponse(await Mediator.Send(userChangePasswordCommand));
}
