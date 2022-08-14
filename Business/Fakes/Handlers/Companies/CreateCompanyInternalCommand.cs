using AutoMapper;
using Business.Handlers.Companies.Commands;
using Business.Handlers.Companies.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using static Business.Handlers.Companies.Commands.CreateCompanyCommand;

namespace Business.Fakes.Handlers.Companies
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCompanyInternalCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public string FirmName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string TaxNo { get; set; }
        public string WebSite { get; set; }



        public class CreateCompanyInternalCommandHandler : IRequestHandler<CreateCompanyInternalCommand, IResult>
        {
            private readonly ICompanyRepository _companyRepository;
            private readonly IMapper _mapper;
            public CreateCompanyInternalCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
            {
                _companyRepository = companyRepository;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(CreateCompanyValidator))]
            [CacheRemoveAspect]
            [LogAspect]
            public async Task<IResult> Handle(CreateCompanyInternalCommand request, CancellationToken cancellationToken)
            {
                var handler = new CreateCompanyCommandHandler(_companyRepository);
                return await handler.Handle(_mapper.Map<CreateCompanyCommand>(request), cancellationToken);
            }
        }
    }
}