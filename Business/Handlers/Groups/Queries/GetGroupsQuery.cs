using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Groups.Queries
{
    public class GetGroupsQuery : IRequest<IDataResult<IEnumerable<Group>>>
    {
        public int Id { get; set; }

        public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IDataResult<IEnumerable<Group>>>
        {
            private readonly IGroupRepository _groupRepository;

            public GetGroupsQueryHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            [CacheAspect(10)]
            public async Task<IDataResult<IEnumerable<Group>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
            {
                var list = await _groupRepository.GetListAsync();
                return new SuccessDataResult<IEnumerable<Group>>(list.ToList());
            }
        }
    }
}