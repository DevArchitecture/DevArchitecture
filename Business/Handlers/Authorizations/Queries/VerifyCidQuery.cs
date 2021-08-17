using System.Threading;
using System.Threading.Tasks;
using Business.Adapters.PersonService;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Dtos;
using MediatR;

namespace Business.Handlers.Authorizations.Queries
{
    public class VerifyCidQuery : IRequest<IDataResult<bool>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long CitizenId { get; set; }
        public int BirthYear { get; set; }

        public class VerifyCidQueryHandler : IRequestHandler<VerifyCidQuery, IDataResult<bool>>
        {
            private readonly IPersonService _personService;

            public VerifyCidQueryHandler(IPersonService personService)
            {
                _personService = personService;
            }

            public async Task<IDataResult<bool>> Handle(VerifyCidQuery request, CancellationToken cancellationToken)
            {
                var result = await _personService.VerifyCid(new Citizen()
                {
                    BirthYear = request.BirthYear,
                    CitizenId = request.CitizenId,
                    Name = request.Name,
                    Surname = request.Surname
                });
                if (!result)
                {
                    return new ErrorDataResult<bool>(result, Messages.CouldNotBeVerifyCid);
                }

                return new SuccessDataResult<bool>(result, Messages.VerifyCid);
            }
        }
    }
}