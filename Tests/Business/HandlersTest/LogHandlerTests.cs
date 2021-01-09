
using Business.Handlers.Logs.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Logs.Queries.GetLogQuery;
using Entities.Concrete;
using static Business.Handlers.Logs.Queries.GetLogsQuery;
using static Business.Handlers.Logs.Commands.CreateLogCommand;
using Business.Handlers.Logs.Commands;
using Business.Constants;
using static Business.Handlers.Logs.Commands.UpdateLogCommand;
using static Business.Handlers.Logs.Commands.DeleteLogCommand;
using MediatR;
using System.Linq;
using Core.Entities.Concrete;

namespace Tests.Business.HandlersTest
{
	[TestFixture]
	public class LogHandlerTests
	{
		Mock<ILogRepository> _logRepository;
		Mock<IMediator> _mediator;
		[SetUp]
		public void Setup()
		{
			_logRepository = new Mock<ILogRepository>();
			_mediator = new Mock<IMediator>();
		}

		[Test]
		public async Task Log_GetQuery_Success()
		{
			//Arrange
			GetLogQuery query = new GetLogQuery();

			_logRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Log, bool>>>()))
						.ReturnsAsync(new Log()
//propertyler buraya yazılacak
//{																		
//LogId = 1,
//LogName = "Test"
//}
);

			GetLogQueryHandler handler = new GetLogQueryHandler(_logRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			Assert.That(x.Success, Is.True);
			//Assert.That(x.Data.LogId, Is.EqualTo(1));

		}

		[Test]
		public async Task Log_GetQueries_Success()
		{
			//Arrange
			GetLogsQuery query = new GetLogsQuery();

			_logRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Log, bool>>>()))
						.ReturnsAsync(new List<Log> { new Log() { /*TODO:propertyler buraya yazılacak LogId = 1, LogName = "test"*/ } });

			GetLogsQueryHandler handler = new GetLogsQueryHandler(_logRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			Assert.That(x.Success, Is.True);
			Assert.That(((List<Log>)x.Data).Count, Is.GreaterThan(1));

		}

		[Test]
		public async Task Log_CreateCommand_Success()
		{
			Log rt = null;
			//Arrange
			CreateLogCommand command = new CreateLogCommand();
			//propertyler buraya yazılacak
			//command.LogName = "deneme";

			_logRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Log, bool>>>()))
						.ReturnsAsync(rt);

			_logRepository.Setup(x => x.Add(It.IsAny<Log>())).Returns(new Log());

			CreateLogCommandHandler handler = new CreateLogCommandHandler(_logRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_logRepository.Verify(x => x.SaveChangesAsync());
			Assert.That(x.Success, Is.True);
			Assert.That(x.Message, Is.EqualTo(Messages.Added));
		}

		[Test]
		public async Task Log_CreateCommand_NameAlreadyExist()
		{
			//Arrange
			CreateLogCommand command = new CreateLogCommand();
			//propertyler buraya yazılacak 
			//command.LogName = "test";

			_logRepository.Setup(x => x.Query())
																										.Returns(new List<Log> { new Log() { /*TODO:propertyler buraya yazılacak LogId = 1, LogName = "test"*/ } }.AsQueryable());



			_logRepository.Setup(x => x.Add(It.IsAny<Log>())).Returns(new Log());

			CreateLogCommandHandler handler = new CreateLogCommandHandler(_logRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			Assert.That(x.Success, Is.False);
			Assert.That(x.Message, Is.EqualTo(Messages.NameAlreadyExist));
		}

		[Test]
		public async Task Log_UpdateCommand_Success()
		{
			//Arrange
			UpdateLogCommand command = new UpdateLogCommand();
			//command.LogName = "test";

			_logRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Log, bool>>>()))
						.ReturnsAsync(new Log() { /*TODO:propertyler buraya yazılacak LogId = 1, LogName = "deneme"*/ });

			_logRepository.Setup(x => x.Update(It.IsAny<Log>())).Returns(new Log());

			UpdateLogCommandHandler handler = new UpdateLogCommandHandler(_logRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_logRepository.Verify(x => x.SaveChangesAsync());
			Assert.That(x.Success, Is.True);
			Assert.That(x.Message, Is.EqualTo(Messages.Updated));
		}

		[Test]
		public async Task Log_DeleteCommand_Success()
		{
			//Arrange
			DeleteLogCommand command = new DeleteLogCommand();

			_logRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Log, bool>>>()))
						.ReturnsAsync(new Log() { /*TODO:propertyler buraya yazılacak LogId = 1, LogName = "deneme"*/});

			_logRepository.Setup(x => x.Delete(It.IsAny<Log>()));

			DeleteLogCommandHandler handler = new DeleteLogCommandHandler(_logRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_logRepository.Verify(x => x.SaveChangesAsync());
			Assert.That(x.Success, Is.True);
			Assert.That(x.Message, Is.EqualTo(Messages.Deleted));
		}
	}
}

