using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

namespace Business.Handlers.UserGroups.Queries
{

	public class GetUserGroupsQuery : IRequest<IDataResult<IEnumerable<UserGroup>>>
	{
		public class GetUserGroupsQueryHandler : IRequestHandler<GetUserGroupsQuery, IDataResult<IEnumerable<UserGroup>>>
		{
			private readonly IUserGroupRepository _userGroupRepository;

			public GetUserGroupsQueryHandler(IUserGroupRepository userGroupRepository)
			{
				_userGroupRepository = userGroupRepository;
			}

			[SecuredOperation(Priority = 1)]
			[CacheAspect(10)]
			[LogAspect(typeof(FileLogger))]

			public async Task<IDataResult<IEnumerable<UserGroup>>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
			{
				return new SuccessDataResult<IEnumerable<UserGroup>>(await _userGroupRepository.GetListAsync());
			}
		}
	}
}
