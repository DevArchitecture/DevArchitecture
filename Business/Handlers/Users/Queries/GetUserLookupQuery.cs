using Core.Utilities.Results;
using DataAccess.Abstract;
using Core.Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Queries
{
	public class GetUserLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
	{
		public class GetUserLookupQueryHandler : IRequestHandler<GetUserLookupQuery, IDataResult<IEnumerable<SelectionItem>>>
		{
			private readonly IUserRepository _userRepository;
			public GetUserLookupQueryHandler(IUserRepository userRepository)
			{
				_userRepository = userRepository;
			}
			public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserLookupQuery request, CancellationToken cancellationToken)
			{
				var list = await _userRepository.GetListAsync(x => x.Status);

				var userLookup = list.Select(x => new SelectionItem()
				{
					Id = x.UserId.ToString(),
					Label = x.FullName
				});
				return new SuccessDataResult<IEnumerable<SelectionItem>>(userLookup);
			}
		}
	}
}
