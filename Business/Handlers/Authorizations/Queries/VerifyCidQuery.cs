using Business.Adapters.PersonService;
using Business.Constants;
using Core.Utilities.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations.Queries
{
    public class VerifyCidQuery : IRequest<IDataResult<bool>>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public long TCKimlikNo { get; set; }
        public int DogumYili { get; set; }

        public class VerifyCidQueryHandler : IRequestHandler<VerifyCidQuery, IDataResult<bool>>
        {
            private readonly IPersonService _personService;

            public VerifyCidQueryHandler(IPersonService personService)
            {
                _personService = personService;
            }

            public async Task<IDataResult<bool>> Handle(VerifyCidQuery request, CancellationToken cancellationToken)
            {
                var result = await _personService.VerifyCid(request.TCKimlikNo, request.Ad, request.Soyad, request.DogumYili);
                if (result != true)
                {
                    return new ErrorDataResult<bool>(result, Messages.CouldNotBeVerifyCid);
                }
                return new SuccessDataResult<bool>(result, Messages.VerifyCid);
            }
        }
    }
}