using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.GroupClaims.Queries;

public class GetGroupClaimsLookupByGroupIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
{
    public int GroupId { get; set; }

    public class GetGroupClaimsLookupByGroupIdQueryHandler : IRequestHandler<GetGroupClaimsLookupByGroupIdQuery,
        IDataResult<IEnumerable<SelectionItem>>>
    {
        private readonly IGroupClaimRepository _groupClaimRepository;

        public GetGroupClaimsLookupByGroupIdQueryHandler(IGroupClaimRepository groupClaimRepository)
        {
            _groupClaimRepository = groupClaimRepository;
        }

        [SecuredOperation(Priority = 1)]
        [LogAspect]
        public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(
            GetGroupClaimsLookupByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _groupClaimRepository.GetGroupClaimsSelectedList(request.GroupId);
            return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
        }
    }
}
