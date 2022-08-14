using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserGroups.Queries;

public class GetUserGroupLookupByUserIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
{
    public int UserId { get; set; }

    public class GetUserGroupLookupByUserIdQueryHandler : IRequestHandler<GetUserGroupLookupByUserIdQuery,
        IDataResult<IEnumerable<SelectionItem>>>
    {
        private readonly IUserGroupRepository _groupClaimRepository;

        public GetUserGroupLookupByUserIdQueryHandler(IUserGroupRepository groupClaimRepository)
        {
            _groupClaimRepository = groupClaimRepository;
        }

        [SecuredOperation]
        [CacheAspect]
        [LogAspect]
        public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserGroupLookupByUserIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _groupClaimRepository.GetUserGroupSelectedList(request.UserId);
            return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
        }
    }
}
