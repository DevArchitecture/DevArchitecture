
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;


namespace Business.Handlers.Companies.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCompanyCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, IResult>
        {
            private readonly ICompanyRepository _companyRepository;
            private readonly IMediator _mediator;

            public DeleteCompanyCommandHandler(ICompanyRepository companyRepository, IMediator mediator)
            {
                _companyRepository = companyRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect()]
            public async Task<IResult> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
            {
                var companyToDelete = _companyRepository.Get(p => p.Id == request.Id);

                _companyRepository.Delete(companyToDelete);
                await _companyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

