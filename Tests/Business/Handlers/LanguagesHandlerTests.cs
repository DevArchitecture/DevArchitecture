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
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using static Business.Handlers.Languages.Commands.CreateLanguageCommand;
using static Business.Handlers.Languages.Commands.DeleteLanguageCommand;
using static Business.Handlers.Languages.Commands.UpdateLanguageCommand;
using static Business.Handlers.Languages.Queries.GetLanguageQuery;
using LookUpHandler = Business.Handlers.Languages.Queries.GetLanguagesLookUpQuery.GetLanguagesLookUpQueryHandler;
using LookUpWithCodeHandler = Business.Handlers.Languages.Queries.GetLanguagesLookUpWithCodeQuery.GetLanguagesLookUpQueryHandler;
using static Business.Handlers.Languages.Queries.GetLanguagesQuery;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class LanguagesHandlerTests
    {
        private Mock<ILanguageRepository> _languageRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _languageRepository = new Mock<ILanguageRepository>();
            _mediator = new Mock<IMediator>();
        }

        // ==================== GetLanguageQuery Tests ====================

        [Test]
        public async Task GetLanguageQuery_Success_ReturnsLanguage()
        {
            var query = new GetLanguageQuery { Id = 1 };
            var expectedLanguage = new Language { Id = 1, Name = "Türkçe", Code = "tr-TR" };

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(expectedLanguage);

            var handler = new GetLanguageQueryHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(query, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().Be(1);
            result.Data.Name.Should().Be("Türkçe");
            result.Data.Code.Should().Be("tr-TR");
        }

        [Test]
        public async Task GetLanguageQuery_NotFound_ReturnsNullData()
        {
            var query = new GetLanguageQuery { Id = 999 };

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync((Language)null);

            var handler = new GetLanguageQueryHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(query, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().BeNull();
        }

        // ==================== GetLanguagesQuery Tests ====================

        [Test]
        public async Task GetLanguagesQuery_Success_ReturnsLanguageList()
        {
            var query = new GetLanguagesQuery();
            var expectedLanguages = new List<Language>
            {
                new() { Id = 1, Name = "Türkçe", Code = "tr-TR" },
                new() { Id = 2, Name = "English", Code = "en-US" },
                new() { Id = 3, Name = "Deutsch", Code = "de-DE" },
            };

            _languageRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(expectedLanguages);

            var handler = new GetLanguagesQueryHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(query, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(3);
        }

        [Test]
        public async Task GetLanguagesQuery_EmptyList_ReturnsEmpty()
        {
            var query = new GetLanguagesQuery();

            _languageRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(new List<Language>());

            var handler = new GetLanguagesQueryHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(query, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().BeEmpty();
        }

        // ==================== GetLanguagesLookUpQuery Tests ====================

        [Test]
        public async Task GetLanguagesLookUpQuery_Success_ReturnsSelectionItems()
        {
            var query = new GetLanguagesLookUpQuery();
            var expectedItems = new List<SelectionItem>
            {
                new() { Id = 1, Label = "Türkçe" },
                new() { Id = 2, Label = "English" },
            };

            _languageRepository.Setup(x => x.GetLanguagesLookUp())
                .ReturnsAsync(expectedItems);

            var handler = new LookUpHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(query, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
        }

        [Test]
        public async Task GetLanguagesLookUpQuery_EmptyList_ReturnsEmpty()
        {
            var query = new GetLanguagesLookUpQuery();

            _languageRepository.Setup(x => x.GetLanguagesLookUp())
                .ReturnsAsync(new List<SelectionItem>());

            var handler = new LookUpHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(query, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().BeEmpty();
        }

        // ==================== GetLanguagesLookUpWithCodeQuery Tests ====================

        [Test]
        public async Task GetLanguagesLookUpWithCodeQuery_Success_ReturnsSelectionItems()
        {
            var query = new GetLanguagesLookUpWithCodeQuery();
            var expectedItems = new List<SelectionItem>
            {
                new() { Id = "tr-TR", Label = "Türkçe" },
                new() { Id = "en-US", Label = "English" },
            };

            _languageRepository.Setup(x => x.GetLanguagesLookUpWithCode())
                .ReturnsAsync(expectedItems);

            var handler = new LookUpWithCodeHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(query, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
        }

        [Test]
        public async Task GetLanguagesLookUpWithCodeQuery_EmptyList_ReturnsEmpty()
        {
            var query = new GetLanguagesLookUpWithCodeQuery();

            _languageRepository.Setup(x => x.GetLanguagesLookUpWithCode())
                .ReturnsAsync(new List<SelectionItem>());

            var handler = new LookUpWithCodeHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(query, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().BeEmpty();
        }

        // ==================== CreateLanguageCommand Tests ====================

        [Test]
        public async Task CreateLanguageCommand_Success_CreatesLanguage()
        {
            var command = new CreateLanguageCommand
            {
                Name = "Français",
                Code = "fr-FR",
            };

            _languageRepository.Setup(x => x.Query())
                .Returns(new List<Language>().AsQueryable());

            _languageRepository.Setup(x => x.Add(It.IsAny<Language>())).Returns(new Language());

            var handler = new CreateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(command, new CancellationToken());

            _languageRepository.Verify(x => x.Add(It.Is<Language>(l =>
                l.Name == "Français" && l.Code == "fr-FR")));
            _languageRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task CreateLanguageCommand_NameAlreadyExists_ReturnsError()
        {
            var command = new CreateLanguageCommand
            {
                Name = "Türkçe",
                Code = "tr-TR",
            };

            _languageRepository.Setup(x => x.Query())
                .Returns(new List<Language>
                {
                    new() { Id = 1, Name = "Türkçe", Code = "tr-TR" },
                }.AsQueryable());

            var handler = new CreateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(command, new CancellationToken());

            _languageRepository.Verify(x => x.Add(It.IsAny<Language>()), Times.Never);
            result.Success.Should().BeFalse();
            result.Message.Should().Be(Messages.NameAlreadyExist);
        }

        // ==================== UpdateLanguageCommand Tests ====================

        [Test]
        public async Task UpdateLanguageCommand_Success_UpdatesLanguage()
        {
            var command = new UpdateLanguageCommand
            {
                Id = 1,
                Name = "UpdatedName",
                Code = "up-UP",
            };

            var existingLanguage = new Language { Id = 1, Name = "OldName", Code = "ol-OL" };

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(existingLanguage);

            _languageRepository.Setup(x => x.Update(It.IsAny<Language>())).Returns(new Language());

            var handler = new UpdateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(command, new CancellationToken());

            _languageRepository.Verify(x => x.Update(It.Is<Language>(l =>
                l.Id == 1 && l.Name == "UpdatedName" && l.Code == "up-UP")));
            _languageRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task UpdateLanguageCommand_NotFound_ThrowsNullReferenceException()
        {
            var command = new UpdateLanguageCommand
            {
                Id = 999,
                Name = "NonExistent",
                Code = "xx-XX",
            };

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync((Language)null);

            var handler = new UpdateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);

            Func<Task> act = () => handler.Handle(command, new CancellationToken());

            await act.Should().ThrowAsync<NullReferenceException>();
        }

        // ==================== DeleteLanguageCommand Tests ====================

        [Test]
        public async Task DeleteLanguageCommand_Success_DeletesLanguage()
        {
            var command = new DeleteLanguageCommand { Id = 1 };
            var languageToDelete = new Language { Id = 1, Name = "Türkçe", Code = "tr-TR" };

            _languageRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(languageToDelete);

            var handler = new DeleteLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(command, new CancellationToken());

            _languageRepository.Verify(x => x.Delete(It.Is<Language>(l => l.Id == 1)));
            _languageRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Deleted);
        }

        [Test]
        public async Task DeleteLanguageCommand_NotFound_DeletesNullWithoutError()
        {
            var command = new DeleteLanguageCommand { Id = 999 };

            _languageRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns((Language)null);

            var handler = new DeleteLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(command, new CancellationToken());

            _languageRepository.Verify(x => x.Delete(It.IsAny<Language>()));
            _languageRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Deleted);
        }

        // ==================== GetLanguageQuery Failure Tests ====================

        [Test]
        public async Task GetLanguageQuery_WhenRepositoryThrows_PropagatesException()
        {
            var query = new GetLanguageQuery { Id = 1 };

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ThrowsAsync(new InvalidOperationException("Database error"));

            var handler = new GetLanguageQueryHandler(_languageRepository.Object, _mediator.Object);

            Func<Task> act = () => handler.Handle(query, new CancellationToken());

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Database error");
        }

        [Test]
        public async Task GetLanguagesQuery_WhenRepositoryThrows_PropagatesException()
        {
            var query = new GetLanguagesQuery();

            _languageRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ThrowsAsync(new InvalidOperationException("Database error"));

            var handler = new GetLanguagesQueryHandler(_languageRepository.Object, _mediator.Object);

            Func<Task> act = () => handler.Handle(query, new CancellationToken());

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Database error");
        }

        [Test]
        public async Task GetLanguagesLookUpQuery_WhenRepositoryThrows_PropagatesException()
        {
            var query = new GetLanguagesLookUpQuery();

            _languageRepository.Setup(x => x.GetLanguagesLookUp())
                .ThrowsAsync(new InvalidOperationException("Database error"));

            var handler = new LookUpHandler(_languageRepository.Object, _mediator.Object);

            Func<Task> act = () => handler.Handle(query, new CancellationToken());

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Database error");
        }

        [Test]
        public async Task GetLanguagesLookUpWithCodeQuery_WhenRepositoryThrows_PropagatesException()
        {
            var query = new GetLanguagesLookUpWithCodeQuery();

            _languageRepository.Setup(x => x.GetLanguagesLookUpWithCode())
                .ThrowsAsync(new InvalidOperationException("Database error"));

            var handler = new LookUpWithCodeHandler(_languageRepository.Object, _mediator.Object);

            Func<Task> act = () => handler.Handle(query, new CancellationToken());

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Database error");
        }

        [Test]
        public async Task CreateLanguageCommand_WhenSaveChangesFails_ThrowsException()
        {
            var command = new CreateLanguageCommand
            {
                Name = "Français",
                Code = "fr-FR",
            };

            _languageRepository.Setup(x => x.Query())
                .Returns(new List<Language>().AsQueryable());

            _languageRepository.Setup(x => x.Add(It.IsAny<Language>())).Returns(new Language());

            _languageRepository.Setup(x => x.SaveChangesAsync())
                .ThrowsAsync(new InvalidOperationException("Save failed"));

            var handler = new CreateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);

            Func<Task> act = () => handler.Handle(command, new CancellationToken());

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Save failed");
        }

        [Test]
        public async Task UpdateLanguageCommand_WhenSaveChangesFails_ThrowsException()
        {
            var command = new UpdateLanguageCommand
            {
                Id = 1,
                Name = "Test",
                Code = "te-TE",
            };

            _languageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Language, bool>>>()))
                .ReturnsAsync(new Language { Id = 1, Name = "Old", Code = "ol-OL" });

            _languageRepository.Setup(x => x.Update(It.IsAny<Language>())).Returns(new Language());

            _languageRepository.Setup(x => x.SaveChangesAsync())
                .ThrowsAsync(new InvalidOperationException("Save failed"));

            var handler = new UpdateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);

            Func<Task> act = () => handler.Handle(command, new CancellationToken());

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Save failed");
        }

        [Test]
        public async Task DeleteLanguageCommand_WhenSaveChangesFails_ThrowsException()
        {
            var command = new DeleteLanguageCommand { Id = 1 };

            _languageRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language { Id = 1, Name = "Türkçe", Code = "tr-TR" });

            _languageRepository.Setup(x => x.SaveChangesAsync())
                .ThrowsAsync(new InvalidOperationException("Save failed"));

            var handler = new DeleteLanguageCommandHandler(_languageRepository.Object, _mediator.Object);

            Func<Task> act = () => handler.Handle(command, new CancellationToken());

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Save failed");
        }

        // ==================== Edge Cases ====================

        [Test]
        public async Task CreateLanguageCommand_NameComparison_DefaultIsCaseSensitive()
        {
            var command = new CreateLanguageCommand
            {
                Name = "türkçe",
                Code = "tr-TR",
            };

            _languageRepository.Setup(x => x.Query())
                .Returns(new List<Language>
                {
                    new() { Id = 1, Name = "Türkçe", Code = "tr-TR" },
                }.AsQueryable());

            var handler = new CreateLanguageCommandHandler(_languageRepository.Object, _mediator.Object);
            var result = await handler.Handle(command, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Added);
        }
    }
}
