using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Groups.Queries
{
    public class SearchGroupsByNameQuery : IRequest<IDataResult<IEnumerable<Group>>>
    {
        public string GroupName { get; set; }

        public class SearchGroupsByNameQueryHandler : IRequestHandler<SearchGroupsByNameQuery, IDataResult<IEnumerable<Group>>>
        {
            private readonly IGroupRepository _groupRepository;

            public SearchGroupsByNameQueryHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Group>>> Handle(SearchGroupsByNameQuery request, CancellationToken cancellationToken)
            {
                var result = BusinessRules.Run(StringLengthMustBeGreaterThanThree(request.GroupName));

                if (result != null)
                {
                    return new ErrorDataResult<IEnumerable<Group>>(result.Message);
                }

                return new SuccessDataResult<IEnumerable<Group>>(
                    await _groupRepository.GetListAsync(
                        x => x.GroupName.ToLower().Contains(request.GroupName.ToLower())));
            }

            private static IResult StringLengthMustBeGreaterThanThree(string searchString)
            {
                if (searchString.Length >= 3)
                {
                    return new SuccessResult();
                }

                return new ErrorResult(Messages.StringLengthMustBeGreaterThanThree);
            }
        }
    }
}