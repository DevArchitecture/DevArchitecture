using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class UsersHandlerTests
    {
        private Mock<IUserRepository> _repository;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IUserRepository>();
            _mapper = new Mock<IMapper>();

            _mapper.Setup(x => x.Map<UserDto>(It.IsAny<User>()))
                .Returns((User u) => new UserDto { UserId = u.UserId, FullName = u.FullName, Email = u.Email });
        }

        [Test]
        public async Task GetUsersQuery_ReturnsAllUsers()
        {
            var users = new List<User>
            {
                new User { UserId = 1, FullName = "User 1", Email = "u1@test.com" },
                new User { UserId = 2, FullName = "User 2", Email = "u2@test.com" }
            }.AsQueryable();
            _repository.Setup(x => x.GetListAsync()).ReturnsAsync(users);

            var handler = new GetUsersQuery.GetUsersQueryHandler(_repository.Object, _mapper.Object);
            var result = await handler.Handle(new GetUsersQuery(), CancellationToken.None);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
        }

        [Test]
        public async Task GetUserQuery_ReturnsUserById()
        {
            var user = new User { UserId = 1, FullName = "Test User", Email = "test@test.com" };
            _repository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<User, bool>>>())).ReturnsAsync(user);

            var handler = new GetUserQuery.GetUserQueryHandler(_repository.Object, _mapper.Object);
            var result = await handler.Handle(new GetUserQuery { UserId = 1 }, CancellationToken.None);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
        }

        [Test]
        public async Task CreateUserCommand_CreatesNewUser()
        {
            _repository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<User, bool>>>())).ReturnsAsync((User)null);
            _repository.Setup(x => x.Add(It.IsAny<User>()));
            _repository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var handler = new CreateUserCommand.CreateUserCommandHandler(_repository.Object);
            var result = await handler.Handle(new CreateUserCommand
            {
                Email = "new@test.com",
                FullName = "New User",
                Password = "password"
            }, CancellationToken.None);

            result.Success.Should().BeTrue();
        }

        [Test]
        public async Task CreateUserCommand_DuplicateEmail_ReturnsError()
        {
            var existingUser = new User { Email = "existing@test.com" };
            _repository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<User, bool>>>())).ReturnsAsync(existingUser);

            var handler = new CreateUserCommand.CreateUserCommandHandler(_repository.Object);
            var result = await handler.Handle(new CreateUserCommand
            {
                Email = "existing@test.com",
                FullName = "Duplicate User"
            }, CancellationToken.None);

            result.Success.Should().BeFalse();
        }

        [Test]
        public async Task DeleteUserCommand_RemovesUser()
        {
            var user = new User { UserId = 1, FullName = "To Delete", Status = true };
            _repository.Setup(x => x.Get(It.IsAny<Expression<System.Func<User, bool>>>())).Returns(user);
            _repository.Setup(x => x.Update(It.IsAny<User>()));
            _repository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var handler = new DeleteUserCommand.DeleteUserCommandHandler(_repository.Object);
            var result = await handler.Handle(new DeleteUserCommand { UserId = 1 }, CancellationToken.None);

            result.Success.Should().BeTrue();
        }
    }
}
