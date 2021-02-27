using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Core.Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

namespace Business.Handlers.UserClaims.Queries
{
    public class GetUserClaimLookupByUserIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
	{
		public int Id { get; set; }
		public class GetUserClaimLookupByUserIdQueryHandler : IRequestHandler<GetUserClaimLookupByUserIdQuery, IDataResult<IEnumerable<SelectionItem>>>
		{
			private readonly IUserClaimRepository _userClaimRepository;
			private readonly IMediator _mediator;

			public GetUserClaimLookupByUserIdQueryHandler(IUserClaimRepository userClaimRepository, IMediator mediator)
			{
				_userClaimRepository = userClaimRepository;
				_mediator = mediator;
			}

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserClaimLookupByUserIdQuery request, CancellationToken cancellationToken)
			{
				var data = await _userClaimRepository.GetUserClaimSelectedList(request.Id);
				return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
			}
		}
	}
}

