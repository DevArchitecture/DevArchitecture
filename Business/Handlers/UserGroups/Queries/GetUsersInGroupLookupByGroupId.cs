using Core.Utilities.Results;
using DataAccess.Abstract;
using Core.Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserGroups.Queries
{
	public class GetUsersInGroupLookupByGroupId : IRequest<IDataResult<IEnumerable<SelectionItem>>>
	{
		public int GroupId { get; set; }

		public class GetUsersInGroupLookupByGroupIdHandler : IRequestHandler<GetUsersInGroupLookupByGroupId, IDataResult<IEnumerable<SelectionItem>>>
		{
            private readonly IUserGroupRepository _userGroupRepository;

			public GetUsersInGroupLookupByGroupIdHandler(IUserGroupRepository userGroupRepository)
			{
				_userGroupRepository = userGroupRepository;
			}

			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUsersInGroupLookupByGroupId request, CancellationToken cancellationToken)
			{
				return new SuccessDataResult<IEnumerable<SelectionItem>>
								(await _userGroupRepository.GetUsersInGroupSelectedListByGroupId(request.GroupId));
			}
		}
	}
}
