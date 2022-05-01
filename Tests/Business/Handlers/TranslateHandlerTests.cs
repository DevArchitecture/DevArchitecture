﻿using Business.Constants;
using Business.Handlers.Translates.Commands;
using Business.Handlers.Translates.Queries;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;

namespace Tests.Business.Handlers;

using static CreateTranslateCommand;
using static DeleteTranslateCommand;
using static GetTranslateQuery;
using static GetTranslatesQuery;
using static UpdateTranslateCommand;

[TestFixture]
public class TranslateHandlerTests
{
    private Mock<ITranslateRepository> _translateRepository;

    [SetUp]
    public void Setup()
    {
        _translateRepository = new Mock<ITranslateRepository>();
    }

    [Test]
    public async Task Translate_GetQuery_Success()
    {
        // Arrange
        var query = new GetTranslateQuery();

        _translateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
            .ReturnsAsync(new Translate());
        // propertyler buraya yazılacak
        // {
        // TranslateId = 1,
        // TranslateName = "Test"
        // }


        var handler = new GetTranslateQueryHandler(_translateRepository.Object);

        // Act
        var x = await handler.Handle(query, new CancellationToken());

        // Asset
        x.Success.Should().BeTrue();
        // x.Data.TranslateId.Should().Be(1);
    }

    [Test]
    public async Task Translate_GetQueries_Success()
    {
        // Arrange
        var query = new GetTranslatesQuery();

        _translateRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
            .ReturnsAsync(new List<Translate>
            {
                new () { Id = 1, Code = "test", LangId = 1, Value = "Deneme" },
                new () { Id = 2, Code = "test", LangId = 2, Value = "Test" }
            });

        var handler = new GetTranslatesQueryHandler(_translateRepository.Object);

        // Act
        var x = await handler.Handle(query, new CancellationToken());

        // Asset
        x.Success.Should().BeTrue();
        ((List<Translate>)x.Data).Count.Should().BeGreaterThan(1);
    }

    [Test]
    public async Task Translate_CreateCommand_Success()
    {
        Translate rt = null;
        // Arrange
        var command = new CreateTranslateCommand();
        // propertyler buraya yazılacak
        // command.TranslateName = "deneme";

        _translateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
            .ReturnsAsync(rt);

        _translateRepository.Setup(x => x.Add(It.IsAny<Translate>())).Returns(new Translate());

        var handler = new CreateTranslateCommandHandler(_translateRepository.Object);
        var x = await handler.Handle(command, new CancellationToken());

        _translateRepository.Verify(x => x.SaveChangesAsync());
        x.Success.Should().BeTrue();
        x.Message.Should().Be(Messages.Added);
    }

    [Test]
    public async Task Translate_CreateCommand_NameAlreadyExist()
    {
        // Arrange
        var command = new CreateTranslateCommand();
        // propertyler buraya yazılacak
        // command.TranslateName = "test";

        _translateRepository.Setup(x => x.Query())
            .Returns(new List<Translate>
            {
                new ()
                {
                    /*TODO:propertyler buraya yazılacak TranslateId = 1, TranslateName = "test"*/
                }
            }.AsQueryable());


        _translateRepository.Setup(x => x.Add(It.IsAny<Translate>())).Returns(new Translate());

        var handler = new CreateTranslateCommandHandler(_translateRepository.Object);
        var x = await handler.Handle(command, new CancellationToken());

        x.Success.Should().BeFalse();
        x.Message.Should().Be(Messages.NameAlreadyExist);
    }

    [Test]
    public async Task Translate_UpdateCommand_Success()
    {
        // Arrange
        var command = new UpdateTranslateCommand();
        // command.TranslateName = "test";

        _translateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
            .ReturnsAsync(new Translate()
            {
                /*TODO:propertyler buraya yazılacak TranslateId = 1, TranslateName = "deneme"*/
            });

        _translateRepository.Setup(x => x.Update(It.IsAny<Translate>())).Returns(new Translate());

        var handler = new UpdateTranslateCommandHandler(_translateRepository.Object);
        var x = await handler.Handle(command, new CancellationToken());

        _translateRepository.Verify(x => x.SaveChangesAsync());
        x.Success.Should().BeTrue();
        x.Message.Should().Be(Messages.Updated);
    }

    [Test]
    public async Task Translate_DeleteCommand_Success()
    {
        // Arrange
        var command = new DeleteTranslateCommand();

        _translateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Translate, bool>>>()))
            .ReturnsAsync(new Translate()
            {
                /*TODO:propertyler buraya yazılacak TranslateId = 1, TranslateName = "deneme"*/
            });

        _translateRepository.Setup(x => x.Delete(It.IsAny<Translate>()));

        var handler = new DeleteTranslateCommandHandler(_translateRepository.Object);
        var x = await handler.Handle(command, new CancellationToken());

        _translateRepository.Verify(x => x.SaveChangesAsync());
        x.Success.Should().BeTrue();
        x.Message.Should().Be(Messages.Deleted);
    }
}
