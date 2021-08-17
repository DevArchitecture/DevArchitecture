using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserGroups.Queries
{
    public class GetUserGroupLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public int UserId { get; set; }

        public class
            GetUserGroupLookupQueryHandler : IRequestHandler<GetUserGroupLookupQuery,
                IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public GetUserGroupLookupQueryHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserGroupLookupQuery request, CancellationToken cancellationToken)
            {
                var data = await _userGroupRepository.GetUserGroupSelectedList(request.UserId);
                return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
            }
        }
    }
}