using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Commands
{
    [SecuredOperation]
    public class UpdateUserCommand : IRequest<IResult>
    {

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public class UpdateAnimalCommandHandler : IRequestHandler<UpdateUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;

            public UpdateAnimalCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            /// <summary>
            /// Handler bir kategorinin var olup olmadığını doğrular
            /// eğer yoksa yeni bir kategorinin güncellenmesine izin verir.
            /// Aspectler her zaman hadler üzerinde kullanılmalıdır.
            /// Aşağıda validation, cacheremove ve log aspect örnekleri kullanılmıştır.
            /// eğer kategori başarıyla eklenmişse sadece mesaj döner.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            //[ValidationAspect(typeof(CreateAnimalValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]

            public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var isUserExits = await _userRepository.GetAsync(u => u.UserId == request.UserId);

                //Tüm alanlar aşağıdaki örnekteki gibi yazılacak
                isUserExits.FullName = request.FullName;
                isUserExits.Email = request.Email;

                _userRepository.Update(isUserExits);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }

}
