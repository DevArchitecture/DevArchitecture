﻿using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserGroups.Queries;

public class GetUserGroupQuery : IRequest<IDataResult<UserGroup>>
{
    public int UserId { get; set; }

    public class GetUserGroupQueryHandler : IRequestHandler<GetUserGroupQuery, IDataResult<UserGroup>>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetUserGroupQueryHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        [SecuredOperation]
        [LogAspect]
        public async Task<IDataResult<UserGroup>> Handle(GetUserGroupQuery request, CancellationToken cancellationToken)
        {
            var userGroup = await _userGroupRepository.GetAsync(p => p.UserId == request.UserId);
            return new SuccessDataResult<UserGroup>(userGroup);
        }
    }
}
