using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Core.Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

namespace Business.Handlers.GroupClaims.Queries
{

	public class GetGroupClaimsLookupByGroupIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
	{
		public int GroupId { get; set; }
		public class GetGroupClaimsLookupByGroupIdQueryHandler : IRequestHandler<GetGroupClaimsLookupByGroupIdQuery, IDataResult<IEnumerable<SelectionItem>>>
		{
			private readonly IGroupClaimRepository _groupClaimRepository;

			public GetGroupClaimsLookupByGroupIdQueryHandler(IGroupClaimRepository groupClaimRepository)
			{
				_groupClaimRepository = groupClaimRepository;
			}

			[SecuredOperation(Priority = 1)]
			[LogAspect(typeof(FileLogger))]
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetGroupClaimsLookupByGroupIdQuery request, CancellationToken cancellationToken)
			{
				var data = await _groupClaimRepository.GetGroupClaimsSelectedList(request.GroupId);
				return new SuccessDataResult<IEnumerable<SelectionItem>>(data);

			}
		}
	}
}
