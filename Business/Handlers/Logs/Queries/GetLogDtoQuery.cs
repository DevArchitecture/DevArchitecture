using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Newtonsoft.Json;

namespace Business.Handlers.Logs.Queries
{
    public class GetLogDtoQuery : IRequest<IDataResult<IEnumerable<LogDto>>>
    {
        public class GetLogDtoQueryHandler : IRequestHandler<GetLogDtoQuery, IDataResult<IEnumerable<LogDto>>>
        {
            private readonly ILogRepository _logRepository;
            private readonly IMediator _mediator;

            public GetLogDtoQueryHandler(ILogRepository logRepository, IMediator mediator)
            {
                _logRepository = logRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<LogDto>>> Handle(GetLogDtoQuery request, CancellationToken cancellationToken)
            {
                var result = await _logRepository.GetListAsync();
                var data = new List<LogDto>();
                foreach (var item in result)
                {
                    var jsonMessage = JsonConvert.DeserializeObject<LogDto>(item.MessageTemplate);
                    dynamic msg = JsonConvert.DeserializeObject(item.MessageTemplate);
                    var valueList = msg.Parameters[0];
                    var exceptionMessage = msg.ExceptionMessage;
                    valueList = valueList.Value.ToString();

                    var list = new LogDto
                    {
                        Id = item.Id,
                        Level = item.Level,
                        TimeStamp = item.TimeStamp,
                        Type = msg.Parameters[0].Type,
                        User = jsonMessage.User,
                        Value = valueList,
                        ExceptionMessage = exceptionMessage
                    };

                    data.Add(list);
                }

                return new SuccessDataResult<IEnumerable<LogDto>>(data);
            }
        }
    }
}