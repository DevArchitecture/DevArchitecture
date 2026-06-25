using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Logs.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using FluentAssertions;
using MediatR;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using static Business.Handlers.Logs.Queries.GetLogDtoQuery;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class LogsHandlerTests
    {
        private Mock<ILogRepository> _logRepository;
        private Mock<IMediator> _mediator;

        private GetLogDtoQueryHandler _getLogDtoQueryHandler;

        [SetUp]
        public void Setup()
        {
            _logRepository = new Mock<ILogRepository>();
            _mediator = new Mock<IMediator>();
            _getLogDtoQueryHandler = new GetLogDtoQueryHandler(_logRepository.Object, _mediator.Object);
        }

        [Test]
        public async Task Handler_GetList()
        {
            var messageTemplate = JsonConvert.SerializeObject(new
            {
                User = "test",
                Parameters = new[] { new { Type = "Info", Value = "test value" } },
                ExceptionMessage = "none"
            });

            var log = new Log
            {
                Id = 1,
                Level = "Information",
                TimeStamp = System.DateTime.Now,
                MessageTemplate = messageTemplate
            };

            _logRepository.Setup(x => x.GetListAsync())
                .ReturnsAsync(new List<Log> { log }.AsQueryable());

            var result = await _getLogDtoQueryHandler
                .Handle(new GetLogDtoQuery(), new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(1);
        }
    }
}
