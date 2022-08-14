
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

            public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
            {
                _companyRepository = companyRepository;
            }

            [SecuredOperation]
            [CacheRemoveAspect]
            [LogAspect]
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

