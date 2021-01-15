
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Dtos;
using Newtonsoft.Json;

namespace Business.Handlers.Logs.Queries
{
	[SecuredOperation]
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

			[PerformanceAspect(5)]
			[CacheAspect(10)]
			[LogAspect(typeof(FileLogger))]
			public async Task<IDataResult<IEnumerable<LogDto>>> Handle(GetLogDtoQuery request, CancellationToken cancellationToken)
			{
				var result = await _logRepository.GetListAsync();
				List<LogDto> data = new List<LogDto>();
				foreach (var item in result)
				{
					var jsonMesssage = JsonConvert.DeserializeObject<LogDto>(item.MessageTemplate);
					dynamic msg = JsonConvert.DeserializeObject(item.MessageTemplate);
					dynamic valueList = msg.Parameters[0];
					dynamic exceptionMessage = msg.ExceptionMessage;
					valueList = valueList.Value.ToString();

					var list = new LogDto
					{
						Id = item.Id,
						Level = item.Level,
						TimeStamp = item.TimeStamp,
						Type = msg.Parameters[0].Type,
						User = jsonMesssage.User,
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