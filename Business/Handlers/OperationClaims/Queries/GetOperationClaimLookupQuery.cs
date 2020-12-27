using Core.Utilities.Results;
using DataAccess.Abstract;
using Core.Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.OperationClaims.Queries
{
	public class GetOperationClaimLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
	{
		public class GetOperationClaimLookupQueryHandler : IRequestHandler<GetOperationClaimLookupQuery, IDataResult<IEnumerable<SelectionItem>>>
		{
			private readonly IOperationClaimRepository _operationClaimRepository;
			public GetOperationClaimLookupQueryHandler(IOperationClaimRepository operationClaimRepository)
			{
				_operationClaimRepository = operationClaimRepository;
			}
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetOperationClaimLookupQuery request, CancellationToken cancellationToken)
			{
				var list = await _operationClaimRepository.GetListAsync();

				var operationClaim = list.Select(x => new SelectionItem()
				{
					Id = x.Id.ToString(),
					Label = x.Alias ?? x.Name
                });
				return new SuccessDataResult<IEnumerable<SelectionItem>>
								(operationClaim);
			}
		}
	}
}
