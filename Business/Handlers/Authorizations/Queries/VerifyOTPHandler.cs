using System.Threading;
using System.Threading.Tasks;
using Business.Services.Authentication;
using Business.Services.Authentication.Model;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Authorizations.Queries
{
    public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, IDataResult<DArchToken>>
    {
        private readonly IAuthenticationCoordinator _coordinator;

        public VerifyOtpHandler(IAuthenticationCoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        /// <summary>
        /// Allows a user to login to the system, back to the browser returns a token stored in local storage.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<DArchToken>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var provider = _coordinator.SelectProvider(request.Provider);
            return await provider.Verify(request);
        }
    }
}