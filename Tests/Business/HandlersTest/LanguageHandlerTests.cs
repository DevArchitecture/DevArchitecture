
using Business.Handlers.Languages.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Languages.Queries.GetLanguageQuery;
using Entities.Concrete;
using static Business.Handlers.Languages.Queries.GetLanguagesQuery;
using static Business.Handlers.Languages.Commands.CreateLanguageCommand;
using Business.Handlers.Languages.Commands;
using Business.Constants;
using static Business.Handlers.Languages.Commands.UpdateLanguageCommand;
using static Business.Handlers.Languages.Commands.DeleteLanguageCommand;
using MediatR;
using System.Linq;
using Core.Entities.Concrete;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LanguageHandlerTests
    {
        Mock<ILanguageRepository> _languageRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _languageRepository = new Mock<ILanguageRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Language_GetQuery_Success()
        {
            //Arrange
            GetLanguageQuery query = new GetLanguageQuery();

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                        .ReturnsAsync(new Language()
//propertyler buraya yazılacak
//{																		
//LanguageId = 1,
//LanguageName = "Test"
//}
);

            GetLanguageQueryHandler handler = new GetLanguageQueryHandler(_languageRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            Assert.That(x.Success, Is.True);
            //Assert.That(x.Data.LanguageId, Is.EqualTo(1));

        }

        [Test]
        public async Task Language_GetQueries_Success()
        {
            //Arrange
            GetLanguagesQuery query = new GetLanguagesQuery();

            _languageRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                        .ReturnsAsync(new List<Language> { new Language() { /*TODO:propertyler buraya yazılacak LanguageId = 1, LanguageName = "test"*/ } });

            GetLanguagesQueryHandler handler = new GetLanguagesQueryHandler(_languageRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            Assert.That(x.Success, Is.True);
            Assert.That(((List<Language>)x.Data).Count, Is.GreaterThan(1));

        }

        [Test]
        public async Task Language_CreateCommand_Success()
        {
            Language rt = null;
            //Arrange
            CreateLanguageCommand command = new CreateLanguageCommand();
            //propertyler buraya yazılacak
            //command.LanguageName = "deneme";

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                        .ReturnsAsync(rt);

            _languageRepository.Setup(x => x.Add(It.IsAny<Language>())).Returns(new Language());

            CreateLanguageCommandHandler handler = new CreateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _languageRepository.Verify(x => x.SaveChangesAsync());
            Assert.That(x.Success, Is.True);
            Assert.That(x.Message, Is.EqualTo(Messages.Added));
        }

        [Test]
        public async Task Language_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            CreateLanguageCommand command = new CreateLanguageCommand();
            //propertyler buraya yazılacak 
            //command.LanguageName = "test";

            _languageRepository.Setup(x => x.Query())
                                                                                                        .Returns(new List<Language> { new Language() { /*TODO:propertyler buraya yazılacak LanguageId = 1, LanguageName = "test"*/ } }.AsQueryable());



            _languageRepository.Setup(x => x.Add(It.IsAny<Language>())).Returns(new Language());

            CreateLanguageCommandHandler handler = new CreateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.That(x.Success, Is.False);
            Assert.That(x.Message, Is.EqualTo(Messages.NameAlreadyExist));
        }

        [Test]
        public async Task Language_UpdateCommand_Success()
        {
            //Arrange
            UpdateLanguageCommand command = new UpdateLanguageCommand();
            //command.LanguageName = "test";

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                        .ReturnsAsync(new Language() { /*TODO:propertyler buraya yazılacak LanguageId = 1, LanguageName = "deneme"*/ });

            _languageRepository.Setup(x => x.Update(It.IsAny<Language>())).Returns(new Language());

            UpdateLanguageCommandHandler handler = new UpdateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _languageRepository.Verify(x => x.SaveChangesAsync());
            Assert.That(x.Success, Is.True);
            Assert.That(x.Message, Is.EqualTo(Messages.Updated));
        }

        [Test]
        public async Task Language_DeleteCommand_Success()
        {
            //Arrange
            DeleteLanguageCommand command = new DeleteLanguageCommand();

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                        .ReturnsAsync(new Language() { /*TODO:propertyler buraya yazılacak LanguageId = 1, LanguageName = "deneme"*/});

            _languageRepository.Setup(x => x.Delete(It.IsAny<Language>()));

            DeleteLanguageCommandHandler handler = new DeleteLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _languageRepository.Verify(x => x.SaveChangesAsync());
            Assert.That(x.Success, Is.True);
            Assert.That(x.Message, Is.EqualTo(Messages.Deleted));
        }
    }
}

