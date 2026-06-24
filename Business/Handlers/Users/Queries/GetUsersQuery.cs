using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Users.Queries
{
    public class GetUsersQuery : BasePaginatedQuery<UserDto>
    {
        public GetUsersQuery(int pageNumber = 1, int pageSize = 10) 
            : base(pageNumber, pageSize) { }
        
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IDataResult<PaginatedResult<IEnumerable<UserDto>>>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<PaginatedResult<IEnumerable<UserDto>>>> Handle(
                GetUsersQuery request, CancellationToken cancellationToken)
            {
                var userList = await _userRepository.GetListAsync();
                var totalCount = userList.Count();
                
                var paginatedUsers = userList
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(user => _mapper.Map<UserDto>(user))
                    .ToList();

                var result = new PaginatedResult<IEnumerable<UserDto>>(
                    paginatedUsers, 
                    request.PageNumber, 
                    request.PageSize);
                
                result.TotalRecords = totalCount;
                result.TotalPages = (int)System.Math.Ceiling(totalCount / (double)request.PageSize);

                return new SuccessDataResult<PaginatedResult<IEnumerable<UserDto>>>(result);
            }
        }
    }
}
