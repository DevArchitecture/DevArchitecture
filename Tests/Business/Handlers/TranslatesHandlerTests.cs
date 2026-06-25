using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.Translates.Commands;
using Business.Handlers.Translates.Queries;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using static Business.Handlers.Translates.Queries.GetTranslatesQuery;
using static Business.Handlers.Translates.Commands.CreateTranslateCommand;
using static Business.Handlers.Translates.Commands.UpdateTranslateCommand;
using static Business.Handlers.Translates.Commands.DeleteTranslateCommand;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class TranslatesHandlerTests
    {
        private Mock<ITranslateRepository> _translateRepository;
        private Mock<IMediator> _mediator;

        private GetTranslatesQueryHandler _getTranslatesQueryHandler;
        private CreateTranslateCommandHandler _createTranslateCommandHandler;
        private UpdateTranslateCommandHandler _updateTranslateCommandHandler;
        private DeleteTranslateCommandHandler _deleteTranslateCommandHandler;

        [SetUp]
        public void Setup()
        {
            _translateRepository = new Mock<ITranslateRepository>();
            _mediator = new Mock<IMediator>();
            _getTranslatesQueryHandler = new GetTranslatesQueryHandler(_translateRepository.Object, _mediator.Object);
            _createTranslateCommandHandler = new CreateTranslateCommandHandler(_translateRepository.Object, _mediator.Object);
            _updateTranslateCommandHandler = new UpdateTranslateCommandHandler(_translateRepository.Object, _mediator.Object);
            _deleteTranslateCommandHandler = new DeleteTranslateCommandHandler(_translateRepository.Object, _mediator.Object);
        }

        [Test]
        public async Task Handler_GetList()
        {
            var translate = new Translate { Id = 1, LangId = 1, Code = "test", Value = "Test" };
            _translateRepository.Setup(x => x.GetListAsync())
                .ReturnsAsync(new List<Translate> { translate }.AsQueryable());

            var result = await _getTranslatesQueryHandler
                .Handle(new GetTranslatesQuery(), new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(1);
        }

        [Test]
        public async Task Handler_CreateTranslate()
        {
            _translateRepository.Setup(x => x.Query()).Returns(new List<Translate>().AsQueryable());

            var command = new CreateTranslateCommand
            {
                LangId = 1,
                Code = "new.code",
                Value = "New Value"
            };

            var result = await _createTranslateCommandHandler
                .Handle(command, new CancellationToken());

            _translateRepository.Verify(x => x.Add(It.IsAny<Translate>()));
            _translateRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Handler_UpdateTranslate()
        {
            var translate = new Translate { Id = 1, LangId = 1, Code = "test", Value = "Test" };
            _translateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<Translate, bool>>>()))
                .ReturnsAsync(translate);

            var command = new UpdateTranslateCommand
            {
                Id = 1,
                LangId = 1,
                Code = "updated.code",
                Value = "Updated Value"
            };

            var result = await _updateTranslateCommandHandler
                .Handle(command, new CancellationToken());

            _translateRepository.Verify(x => x.Update(It.IsAny<Translate>()));
            _translateRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Handler_DeleteTranslate()
        {
            var translate = new Translate { Id = 1, LangId = 1, Code = "test", Value = "Test" };
            _translateRepository.Setup(x => x.Get(It.IsAny<Expression<System.Func<Translate, bool>>>()))
                .Returns(translate);

            var command = new DeleteTranslateCommand
            {
                Id = 1
            };

            var result = await _deleteTranslateCommandHandler
                .Handle(command, new CancellationToken());

            _translateRepository.Verify(x => x.Delete(It.IsAny<Translate>()));
            _translateRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Deleted);
        }
    }
}
