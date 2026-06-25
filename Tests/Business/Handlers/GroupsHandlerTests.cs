using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.Groups.Commands;
using Business.Handlers.Groups.Queries;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Business.Handlers.Groups.Queries.GetGroupsQuery;
using static Business.Handlers.Groups.Queries.GetGroupQuery;
using static Business.Handlers.Groups.Commands.CreateGroupCommand;
using static Business.Handlers.Groups.Commands.UpdateGroupCommand;
using static Business.Handlers.Groups.Commands.DeleteGroupCommand;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class GroupsHandlerTests
    {
        private Mock<IGroupRepository> _groupRepository;

        private GetGroupsQueryHandler _getGroupsQueryHandler;
        private GetGroupQueryHandler _getGroupQueryHandler;
        private CreateGroupCommandHandler _createGroupCommandHandler;
        private UpdateGroupCommandHandler _updateGroupCommandHandler;
        private DeleteGroupCommandHandler _deleteGroupCommandHandler;

        [SetUp]
        public void Setup()
        {
            _groupRepository = new Mock<IGroupRepository>();
            _getGroupsQueryHandler = new GetGroupsQueryHandler(_groupRepository.Object);
            _getGroupQueryHandler = new GetGroupQueryHandler(_groupRepository.Object);
            _createGroupCommandHandler = new CreateGroupCommandHandler(_groupRepository.Object);
            _updateGroupCommandHandler = new UpdateGroupCommandHandler(_groupRepository.Object);
            _deleteGroupCommandHandler = new DeleteGroupCommandHandler(_groupRepository.Object);
        }

        [Test]
        public async Task Handler_GetList()
        {
            var group = new Group { Id = 1, GroupName = "TestGroup" };
            _groupRepository.Setup(x => x.GetListAsync())
                .ReturnsAsync(new List<Group> { group }.AsQueryable());

            var result = await _getGroupsQueryHandler
                .Handle(new GetGroupsQuery(), new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(1);
        }

        [Test]
        public async Task Handler_GetGroup()
        {
            var group = new Group { Id = 1, GroupName = "TestGroup" };
            _groupRepository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<Group, bool>>>()))
                .ReturnsAsync(group);

            var result = await _getGroupQueryHandler
                .Handle(new GetGroupQuery { GroupId = 1 }, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().Be(1);
        }

        [Test]
        public async Task Handler_CreateGroup()
        {
            var command = new CreateGroupCommand
            {
                GroupName = "NewGroup"
            };

            var result = await _createGroupCommandHandler
                .Handle(command, new CancellationToken());

            _groupRepository.Verify(x => x.Add(It.IsAny<Group>()));
            _groupRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Handler_UpdateGroup()
        {
            var command = new UpdateGroupCommand
            {
                Id = 1,
                GroupName = "UpdatedGroup"
            };

            var result = await _updateGroupCommandHandler
                .Handle(command, new CancellationToken());

            _groupRepository.Verify(x => x.Update(It.IsAny<Group>()));
            _groupRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Handler_DeleteGroup()
        {
            var group = new Group { Id = 1, GroupName = "TestGroup" };
            _groupRepository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<Group, bool>>>()))
                .ReturnsAsync(group);

            var command = new DeleteGroupCommand
            {
                Id = 1
            };

            var result = await _deleteGroupCommandHandler
                .Handle(command, new CancellationToken());

            _groupRepository.Verify(x => x.Delete(It.IsAny<Group>()));
            _groupRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Deleted);
        }
    }
}
