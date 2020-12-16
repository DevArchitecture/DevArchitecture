using Business.Services.Authentication;
using Business.Services.Authentication.Model;
using Core.Utilities.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations.Queries
{

    public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, IDataResult<SFwToken>>
    {
        private readonly IAuthenticationCoordinator _coordinator;

        public VerifyOtpHandler(IAuthenticationCoordinator coordinator)
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
        // [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<SFwToken>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            // Uygun providerı al ve login ol.
            var provider = _coordinator.SelectProvider(request.Provider);
            return await provider.Verify(request);
        }
    }
}
