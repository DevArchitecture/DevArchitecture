using System.Threading.Tasks;
using DataAccess.Abstract;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class LogHandlerTests
    {
        private Mock<ILogRepository> _logRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _logRepository = new Mock<ILogRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public Task Log_GetQuery_Success()
        {
            return Task.CompletedTask;
        }
    }
}