
using Business.Handlers.Translates.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Translates.Queries.GetTranslateQuery;
using Entities.Concrete;
using static Business.Handlers.Translates.Queries.GetTranslatesQuery;
using static Business.Handlers.Translates.Commands.CreateTranslateCommand;
using Business.Handlers.Translates.Commands;
using Business.Constants;
using static Business.Handlers.Translates.Commands.UpdateTranslateCommand;
using static Business.Handlers.Translates.Commands.DeleteTranslateCommand;
using MediatR;
using System.Linq;
using Core.Entities.Concrete;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class TranslateHandlerTests
    {
        Mock<ITranslateRepository> _translateRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _translateRepository = new Mock<ITranslateRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Translate_GetQuery_Success()
        {
            //Arrange
            GetTranslateQuery query = new GetTranslateQuery();

            _translateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
                        .ReturnsAsync(new Translate()
//propertyler buraya yazılacak
//{																		
//TranslateId = 1,
//TranslateName = "Test"
//}
);

            GetTranslateQueryHandler handler = new GetTranslateQueryHandler(_translateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            Assert.That(x.Success, Is.True);
            //Assert.That(x.Data.TranslateId, Is.EqualTo(1));

        }

        [Test]
        public async Task Translate_GetQueries_Success()
        {
            //Arrange
            GetTranslatesQuery query = new GetTranslatesQuery();

            _translateRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
                        .ReturnsAsync(new List<Translate> { new Translate() { /*TODO:propertyler buraya yazılacak TranslateId = 1, TranslateName = "test"*/ } });

            GetTranslatesQueryHandler handler = new GetTranslatesQueryHandler(_translateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            Assert.That(x.Success, Is.True);
            Assert.That(((List<Translate>)x.Data).Count, Is.GreaterThan(1));

        }

        [Test]
        public async Task Translate_CreateCommand_Success()
        {
            Translate rt = null;
            //Arrange
            CreateTranslateCommand command = new CreateTranslateCommand();
            //propertyler buraya yazılacak
            //command.TranslateName = "deneme";

            _translateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
                        .ReturnsAsync(rt);

            _translateRepository.Setup(x => x.Add(It.IsAny<Translate>())).Returns(new Translate());

            CreateTranslateCommandHandler handler = new CreateTranslateCommandHandler(_translateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _translateRepository.Verify(x => x.SaveChangesAsync());
            Assert.That(x.Success, Is.True);
            Assert.That(x.Message, Is.EqualTo(Messages.Added));
        }

        [Test]
        public async Task Translate_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            CreateTranslateCommand command = new CreateTranslateCommand();
            //propertyler buraya yazılacak 
            //command.TranslateName = "test";

            _translateRepository.Setup(x => x.Query())
                                                                                                        .Returns(new List<Translate> { new Translate() { /*TODO:propertyler buraya yazılacak TranslateId = 1, TranslateName = "test"*/ } }.AsQueryable());



            _translateRepository.Setup(x => x.Add(It.IsAny<Translate>())).Returns(new Translate());

            CreateTranslateCommandHandler handler = new CreateTranslateCommandHandler(_translateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.That(x.Success, Is.False);
            Assert.That(x.Message, Is.EqualTo(Messages.NameAlreadyExist));
        }

        [Test]
        public async Task Translate_UpdateCommand_Success()
        {
            //Arrange
            UpdateTranslateCommand command = new UpdateTranslateCommand();
            //command.TranslateName = "test";

            _translateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
                        .ReturnsAsync(new Translate() { /*TODO:propertyler buraya yazılacak TranslateId = 1, TranslateName = "deneme"*/ });

            _translateRepository.Setup(x => x.Update(It.IsAny<Translate>())).Returns(new Translate());

            UpdateTranslateCommandHandler handler = new UpdateTranslateCommandHandler(_translateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _translateRepository.Verify(x => x.SaveChangesAsync());
            Assert.That(x.Success, Is.True);
            Assert.That(x.Message, Is.EqualTo(Messages.Updated));
        }

        [Test]
        public async Task Translate_DeleteCommand_Success()
        {
            //Arrange
            DeleteTranslateCommand command = new DeleteTranslateCommand();

            _translateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
                        .ReturnsAsync(new Translate() { /*TODO:propertyler buraya yazılacak TranslateId = 1, TranslateName = "deneme"*/});

            _translateRepository.Setup(x => x.Delete(It.IsAny<Translate>()));

            DeleteTranslateCommandHandler handler = new DeleteTranslateCommandHandler(_translateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _translateRepository.Verify(x => x.SaveChangesAsync());
            Assert.That(x.Success, Is.True);
            Assert.That(x.Message, Is.EqualTo(Messages.Deleted));
        }
    }
}

