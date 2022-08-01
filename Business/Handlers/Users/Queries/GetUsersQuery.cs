using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Users.Queries;

public class GetUsersQuery : IRequest<IDataResult<IEnumerable<UserDto>>>
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IDataResult<IEnumerable<UserDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper, IMediator mediator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [SecuredOperation(Priority = 1)]
        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect()]
        public async Task<IDataResult<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _mediator.Send(new GetTenantQuery());
            if (tenant != null && tenant.Data.UserId == 1)
            {
                var users = await _userRepository.GetListAsync();
                var userDtos = users.Select(user => _mapper.Map<UserDto>(user)).ToList();

                return new SuccessDataResult<IEnumerable<UserDto>>(userDtos);
            }
            var userList = await _userRepository.GetListAsync(x => x.TenantId == tenant.Data.TenantId);
            var userDtoList = userList.Select(user => _mapper.Map<UserDto>(user)).ToList();
            return new SuccessDataResult<IEnumerable<UserDto>>(userDtoList);

        }
    }
}
