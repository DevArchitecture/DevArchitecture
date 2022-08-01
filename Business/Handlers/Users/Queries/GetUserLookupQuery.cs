using Business.BusinessAspects;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Users.Queries;

public class GetUserLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
{
    public class
        GetUserLookupQueryHandler : IRequestHandler<GetUserLookupQuery, IDataResult<IEnumerable<SelectionItem>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        public GetUserLookupQueryHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [SecuredOperation(Priority = 1)]
        [CacheAspect(10)]
        [LogAspect()]
        public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserLookupQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _mediator.Send(new GetTenantQuery());
            if (tenant != null && tenant.Data.UserId == 1)
            {
                var userLookups = await _userRepository.GetListAsync(x => x.Status);
                var users = userLookups.Select(x => new SelectionItem() { Id = x.UserId.ToString(), Label = x.FullName });
                return new SuccessDataResult<IEnumerable<SelectionItem>>(users);
            }
            var list = await _userRepository.GetListAsync(x => x.Status && x.TenantId == tenant.Data.TenantId);
            var userLookup = list.Select(x => new SelectionItem() { Id = x.UserId.ToString(), Label = x.FullName });
            return new SuccessDataResult<IEnumerable<SelectionItem>>(userLookup);          
        }
    }
}
