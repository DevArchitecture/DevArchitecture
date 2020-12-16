using Business.Services.Authentication;
using Business.Services.Authentication.Model;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
        /// User Login Handler sınıfıdır. Aspectler bu metodun üzerinde kullanılır.
        /// Bir kullanıcının sisteme login olmasını sağlar geriye browser local storageda saklanan bir token döner 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ValidationAspect(typeof(LoginUserValidator), Priority = 1)]
        // [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<LoginUserResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Uygun providerı al ve login ol.
            var provider = _coordinator.SelectProvider(request.Provider);
            return new SuccessDataResult<LoginUserResult>(await provider.Login(request));
        }
    }
}
