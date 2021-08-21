using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.UserGroups.Commands;
using Business.Handlers.UserGroups.Queries;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.Business.Handlers
{
    using static CreateUserGroupCommand;
    using static DeleteUserGroupCommand;
    using static GetUserGroupsQuery;
    using static UpdateUserGroupCommand;

    [TestFixture]
    public class UserGroupsTests
    {
        private Mock<IUserGroupRepository> _userGroupRepository;

        private GetUserGroupsQueryHandler _getUserGroupsQueryHandler;
        private CreateUserGroupCommandHandler _createUserGroupCommandHandler;
        private UpdateUserGroupCommandHandler _updateUserGroupCommandHandler;
        private DeleteUserGroupCommandHandler _deleteUserGroupCommandHandler;

        [SetUp]
        public void Setup()
        {
            _userGroupRepository = new Mock<IUserGroupRepository>();
            _getUserGroupsQueryHandler = new GetUserGroupsQueryHandler(_userGroupRepository.Object);
            _createUserGroupCommandHandler = new CreateUserGroupCommandHandler(_userGroupRepository.Object);
            _updateUserGroupCommandHandler = new UpdateUserGroupCommandHandler(_userGroupRepository.Object);
            _deleteUserGroupCommandHandler = new DeleteUserGroupCommandHandler(_userGroupRepository.Object);
        }

        [Test]
        public void Handler_GetList()
        {
            var userGroup = new UserGroup() { GroupId = 1, UserId = 1 };
            _userGroupRepository.Setup(x => x.GetListAsync(null))
                .ReturnsAsync(new List<UserGroup>() { userGroup }.AsQueryable());

            var result = _getUserGroupsQueryHandler
                .Handle(new GetUserGroupsQuery(), new CancellationToken()).Result;
            result.Data.Should().HaveCount(1);
        }

        [Test]
        public void Handler_CreateUserGroup()
        {
            var createUserCommand = new CreateUserGroupCommand();
            createUserCommand.UserId = 1;
            createUserCommand.GroupId = 1;

            var result = _createUserGroupCommandHandler
                .Handle(createUserCommand, new CancellationToken()).Result;
            result.Success.Should().BeTrue();
        }

        [Test]
        public void Handler_UpdateUserGroup()
        {
            var updateUserCommand = new UpdateUserGroupCommand();
            updateUserCommand.GroupId = new int[] { 1 };
            updateUserCommand.UserId = 1;

            var result = _updateUserGroupCommandHandler
                .Handle(updateUserCommand, new CancellationToken()).Result;

            result.Success.Should().BeTrue();
        }

        [Test]
        public void Handler_DeleteUser()
        {
            var deleteUserCommand = new DeleteUserGroupCommand();
            var result = _deleteUserGroupCommandHandler
                .Handle(deleteUserCommand, new CancellationToken()).Result;

            result.Success.Should().BeTrue();
        }

        [Test]
        [TransactionScopeAspectAsync]
        public async Task Handler_TransactionScopeAspectAsyncTest()
        {
            await SomeMethodInTheCallStackAsync().ConfigureAwait(false);
        }

        private static async Task SomeMethodInTheCallStackAsync()
        {
            const int delayAmount = 500;

            await Task.Delay(delayAmount).ConfigureAwait(false);
        }
    }
}