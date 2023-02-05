using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.Languages.Commands;
using Business.Handlers.Languages.Queries;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using static Business.Handlers.Languages.Commands.CreateLanguageCommand;
using static Business.Handlers.Languages.Commands.DeleteLanguageCommand;
using static Business.Handlers.Languages.Queries.GetLanguageQuery;
using static Business.Handlers.Languages.Queries.GetLanguagesQuery;
using static Business.Handlers.Languages.Commands.UpdateLanguageCommand;


namespace Tests.Business.Handlers
{
   

    [TestFixture]
    public class LanguageHandlerTests
    {
        private Mock<ILanguageRepository> _languageRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _languageRepository = new Mock<ILanguageRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Language_GetQuery_Success()
        {
            // Arrange
            var query = new GetLanguageQuery();

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(new Language());
// propertyler buraya yazılacak
// {
// LanguageId = 1,
// LanguageName = "Test"
// }


            var handler = new GetLanguageQueryHandler(_languageRepository.Object, _mediator.Object);

            // Act
            var x = await handler.Handle(query, new CancellationToken());

            // Asset
            x.Success.Should().BeTrue();
            // x.Data.Id.Should().Be(1);
        }

        [Test]
        public async Task Language_GetQueries_Success()
        {
            // Arrange
            var query = new GetLanguagesQuery();

            _languageRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(new List<Language>
                {
                    new () { Id = 1, Code = "tr-TR", Name = "Türkçe" },
                    new () { Id = 2, Code = "en-US", Name = "English" }
                });

            var handler = new GetLanguagesQueryHandler(_languageRepository.Object, _mediator.Object);

            // Act
            var x = await handler.Handle(query, new CancellationToken());

            // Asset
            x.Success.Should().BeTrue();
            ((List<Language>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task Language_CreateCommand_Success()
        {
            Language rt = null;
            // Arrange
            var command = new CreateLanguageCommand();
            // propertyler buraya yazılacak
            // command.LanguageName = "deneme";

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(rt);

            _languageRepository.Setup(x => x.Add(It.IsAny<Language>())).Returns(new Language());

            var handler = new CreateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _languageRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Language_CreateCommand_NameAlreadyExist()
        {
            // Arrange
            var command = new CreateLanguageCommand();
            // propertyler buraya yazılacak
            // command.LanguageName = "test";

            _languageRepository.Setup(x => x.Query())
                .Returns(new List<Language>
                {
                    new ()
                    {
                        /*TODO:propertyler buraya yazılacak LanguageId = 1, LanguageName = "test"*/
                    }
                }.AsQueryable());

            _languageRepository.Setup(x => x.Add(It.IsAny<Language>())).Returns(new Language());

            var handler = new CreateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Language_UpdateCommand_Success()
        {
            // Arrange
            var command = new UpdateLanguageCommand();
            // command.LanguageName = "test";

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(new Language()
                {
                    /*TODO:propertyler buraya yazılacak LanguageId = 1, LanguageName = "deneme"*/
                });

            _languageRepository.Setup(x => x.Update(It.IsAny<Language>())).Returns(new Language());

            var handler = new UpdateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _languageRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Language_DeleteCommand_Success()
        {
            // Arrange
            var command = new DeleteLanguageCommand();

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(new Language()
                {
                    /*TODO:propertyler buraya yazılacak LanguageId = 1, LanguageName = "deneme"*/
                });

            _languageRepository.Setup(x => x.Delete(It.IsAny<Language>()));

            var handler = new DeleteLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _languageRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}