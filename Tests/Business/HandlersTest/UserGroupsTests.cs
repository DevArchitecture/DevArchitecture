using Business.Handlers.UserGroups.Commands;
using Business.Handlers.UserGroups.Queries;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Business.Handlers.UserGroups.Commands.CreateUserGroupCommand;
using static Business.Handlers.UserGroups.Commands.DeleteUserGroupCommand;
using static Business.Handlers.UserGroups.Commands.UpdateUserGroupCommand;
using static Business.Handlers.UserGroups.Queries.GetUserGroupsQuery;

namespace SennedjemTests.Business.HandlersTest
{
    [TestFixture]
    public class UserGroupsTests
    {
        Mock<IUserGroupRepository> _userGroupRepository;

        GetUserGroupsQueryHandler getUserGroupsQueryHandler;
        CreateUserGroupCommandHandler createUserGroupCommandHandler;
        UpdateUserGroupCommandHandler updateUserGroupCommandHandler;
        DeleteUserGroupCommandHandler deleteUserGroupCommandHandler;


        [SetUp]
        public void Setup()
        {
            _userGroupRepository = new Mock<IUserGroupRepository>();
            getUserGroupsQueryHandler = new GetUserGroupsQueryHandler(_userGroupRepository.Object);
            createUserGroupCommandHandler = new CreateUserGroupCommandHandler(_userGroupRepository.Object);
            updateUserGroupCommandHandler = new UpdateUserGroupCommandHandler(_userGroupRepository.Object);
            deleteUserGroupCommandHandler = new DeleteUserGroupCommandHandler(_userGroupRepository.Object);
        }

        [Test]
        public void Handler_GetList()
        {
            var userGroup = new UserGroup() { GroupId = 1, UserId = 1 };
            _userGroupRepository.Setup(x => x.GetListAsync(null)).
                ReturnsAsync(new List<UserGroup>() { userGroup }.AsQueryable());

            var result = getUserGroupsQueryHandler.Handle(new GetUserGroupsQuery(), new System.Threading.CancellationToken()).Result;
            Assert.That(result.Data.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Handler_CreateUserGroup()
        {
            var createuserCommand = new CreateUserGroupCommand();
            createuserCommand.UserId = 1;
            createuserCommand.GroupId = 1;

            var result = createUserGroupCommandHandler.Handle(createuserCommand, new System.Threading.CancellationToken()).Result;
            Assert.That(result.Success, Is.True);

        }

        [Test]
        public void Handler_UpdateUserGroup()
        {
            var updateUserCommand = new UpdateUserGroupCommand();
            updateUserCommand.GroupId = new int[] { 1 };
            updateUserCommand.UserId = 1;

            var result = updateUserGroupCommandHandler.
                Handle(updateUserCommand, new System.Threading.CancellationToken()).Result;

            Assert.That(result.Success, Is.True);

        }

        [Test]
        public void Handler_DeleteUser()
        {
            var deleteUserCommand = new DeleteUserGroupCommand();
            var result = deleteUserGroupCommandHandler.
                Handle(deleteUserCommand, new System.Threading.CancellationToken()).Result;

            Assert.That(result.Success, Is.True);
        }

        [Test]
        [TransactionScopeAspectAsync]
        public async Task Handler_TransactionScopeAspectAsyncTest()
        {
            await SomeMethodInTheCallStackAsync().ConfigureAwait(false);

        }

        private static async Task SomeMethodInTheCallStackAsync()
        {
            await Task.Delay(500).ConfigureAwait(false);
        }
    }
}
