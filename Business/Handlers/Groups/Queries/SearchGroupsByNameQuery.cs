
using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Groups.Queries
{
    [SecuredOperation]
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

            public async Task<IDataResult<IEnumerable<Group>>> Handle(SearchGroupsByNameQuery request, CancellationToken cancellationToken)
            {
                var result = BusinessRules.Run(StringLengthMustBeGreaterThanThree(request.GroupName));

                if (result != null)
                    return new ErrorDataResult<IEnumerable<Group>>(result.Message);

                return new SuccessDataResult<IEnumerable<Group>>(await _groupRepository.GetListAsync(x => x.GroupName.ToLower().Contains(request.GroupName.ToLower())));
            }
            private IResult StringLengthMustBeGreaterThanThree(string searchString)
            {
                if (searchString.Length >= 3)
                    return new SuccessResult();

                return new ErrorResult(Messages.StringLengthMustBeGreaterThanThree);
            }
        }
    }
}
