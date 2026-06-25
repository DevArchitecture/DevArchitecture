using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.Handlers.Users.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class RateLimitTests
    {
        [Test]
        public void GetUsersQuery_DefaultPageNumber_IsOne()
        {
            var query = new GetUsersQuery();
            query.PageNumber.Should().Be(1);
        }

        [Test]
        public void GetUsersQuery_DefaultPageSize_IsTen()
        {
            var query = new GetUsersQuery();
            query.PageSize.Should().Be(10);
        }

        [Test]
        public void GetUsersQuery_PageNumber_LessThanOne_SetsToOne()
        {
            var query = new GetUsersQuery(0, 10);
            query.PageNumber.Should().Be(1);
        }

        [Test]
        public void GetUsersQuery_PageSize_GreaterThanMax_SetsToMax()
        {
            var query = new GetUsersQuery(1, 200);
            query.PageSize.Should().Be(100);
        }

        [Test]
        public void GetUsersQuery_PageSize_LessThanOne_SetsToDefault()
        {
            var query = new GetUsersQuery(1, 0);
            query.PageSize.Should().Be(10);
        }

        [Test]
        public async Task GetUsersQueryHandler_ReturnsPaginatedResult()
        {
            var repoMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            var users = new List<User>
            {
                new User { UserId = 1, FullName = "User 1", Email = "user1@test.com" },
                new User { UserId = 2, FullName = "User 2", Email = "user2@test.com" }
            }.AsQueryable();

            repoMock.Setup(x => x.GetListAsync()).ReturnsAsync(users);

            mapperMock.Setup(x => x.Map<UserDto>(It.IsAny<User>()))
                .Returns((User u) => new UserDto { UserId = u.UserId, FullName = u.FullName });

            var handler = new GetUsersQuery.GetUsersQueryHandler(repoMock.Object, mapperMock.Object);
            var query = new GetUsersQuery(1, 10);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
        }
    }
}
