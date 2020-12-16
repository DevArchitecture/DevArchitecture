using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Toolkit;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations.Commands
{
    [SecuredOperation]
    public class ForgotPasswordCommand : IRequest<IResult>
    {
        public string TCKimlikNo { get; set; }
        public string Email { get; set; }

        public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, IResult>
        {

            private readonly IUserRepository _userRepository;

            public ForgotPasswordCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            /// <summary>
            /// Handler bir kategorinin var olup olmadığını doğrular
            /// eğer yoksa yeni bir kategorinin güncellenmesine izin verir.
            /// Aspectler her zaman hadler üzerinde kullanılmalıdır.
            /// Aşağıda validation, cacheremove ve log aspect örnekleri kullanılmıştır.
            /// eğer kategori başarıyla eklenmişse sadece mesaj döner.
            ///  /*user.PasswordHash = passwordHash,
            ///    user.PasswordSalt = passwordSalt,*/
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            //[ValidationAspect(typeof(CreateBagisciValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.CitizenId == Convert.ToInt64(request.TCKimlikNo));

                if (user == null)
                    return new ErrorResult(Messages.WrongCID);
                var generatedPassword = RandomPassword.CreateRandomPassword(14);
                HashingHelper.CreatePasswordHash(generatedPassword, out byte[] passwordSalt, out byte[] passwordHash);

                _userRepository.Update(user);

                return new SuccessResult(Messages.SendPassword + " Yeni Parola:" + generatedPassword);
            }
        }
    }
}
