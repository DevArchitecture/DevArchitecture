﻿using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.GroupClaims.Queries;

public class GetGroupClaimsQuery : IRequest<IDataResult<IEnumerable<GroupClaim>>>
{
    public class
        GetGroupClaimsQueryHandler : IRequestHandler<GetGroupClaimsQuery, IDataResult<IEnumerable<GroupClaim>>>
    {
        private readonly IGroupClaimRepository _groupClaimRepository;

        public GetGroupClaimsQueryHandler(IGroupClaimRepository groupClaimRepository)
        {
            _groupClaimRepository = groupClaimRepository;
        }

        [SecuredOperation]
        [CacheAspect]
        [LogAspect]
        public async Task<IDataResult<IEnumerable<GroupClaim>>> Handle(GetGroupClaimsQuery request, CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<GroupClaim>>(await _groupClaimRepository.GetListAsync());
        }
    }
}
