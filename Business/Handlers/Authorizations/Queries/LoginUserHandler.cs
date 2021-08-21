using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Authorizations.ValidationRules;
using Business.Services.Authentication;
using Business.Services.Authentication.Model;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Authorizations.Queries
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, IDataResult<LoginUserResult>>
    {
        private readonly IAuthenticationCoordinator _coordinator;

        public LoginUserHandler(IAuthenticationCoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        /// <summary>
        /// Allows a user to login to the system, back to the browser returns a token stored in local storage.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ValidationAspect(typeof(LoginUserValidator), Priority = 1)]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<LoginUserResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var provider = _coordinator.SelectProvider(request.Provider);
            return new SuccessDataResult<LoginUserResult>(await provider.Login(request));
        }
    }
}