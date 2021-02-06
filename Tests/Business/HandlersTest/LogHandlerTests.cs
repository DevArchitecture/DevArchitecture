using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using MediatR;

namespace Tests.Business.HandlersTest
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
		public  Task Log_GetQuery_Success()
		{
            return Task.CompletedTask;
        }

	}
}

