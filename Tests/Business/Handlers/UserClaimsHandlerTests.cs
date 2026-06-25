using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.UserClaims.Commands;
using Business.Handlers.UserClaims.Queries;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Business.Handlers.UserClaims.Queries.GetUserClaimsQuery;
using static Business.Handlers.UserClaims.Commands.CreateUserClaimCommand;
using static Business.Handlers.UserClaims.Commands.UpdateUserClaimCommand;
using static Business.Handlers.UserClaims.Commands.DeleteUserClaimCommand;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class UserClaimsHandlerTests
    {
        private Mock<IUserClaimRepository> _userClaimRepository;
        private Mock<ICacheManager> _cacheManager;

        private GetUserClaimsQueryHandler _getUserClaimsQueryHandler;
        private CreateUserClaimCommandHandler _createUserClaimCommandHandler;
        private UpdateUserClaimCommandHandler _updateUserClaimCommandHandler;
        private DeleteUserClaimCommandHandler _deleteUserClaimCommandHandler;

        [SetUp]
        public void Setup()
        {
            _userClaimRepository = new Mock<IUserClaimRepository>();
            _cacheManager = new Mock<ICacheManager>();
            _getUserClaimsQueryHandler = new GetUserClaimsQueryHandler(_userClaimRepository.Object);
            _createUserClaimCommandHandler = new CreateUserClaimCommandHandler(_userClaimRepository.Object, _cacheManager.Object);
            _updateUserClaimCommandHandler = new UpdateUserClaimCommandHandler(_userClaimRepository.Object, _cacheManager.Object);
            _deleteUserClaimCommandHandler = new DeleteUserClaimCommandHandler(_userClaimRepository.Object);
        }

        [Test]
        public async Task Handler_GetList()
        {
            var userClaim = new UserClaim { UserId = 1, ClaimId = 1 };
            _userClaimRepository.Setup(x => x.GetListAsync())
                .ReturnsAsync(new List<UserClaim> { userClaim }.AsQueryable());

            var result = await _getUserClaimsQueryHandler
                .Handle(new GetUserClaimsQuery(), new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(1);
        }

        [Test]
        public async Task Handler_CreateUserClaim()
        {
            var command = new CreateUserClaimCommand
            {
                UserId = 1,
                ClaimId = 1
            };

            var result = await _createUserClaimCommandHandler
                .Handle(command, new CancellationToken());

            _userClaimRepository.Verify(x => x.Add(It.IsAny<UserClaim>()));
            _userClaimRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Handler_UpdateUserClaim()
        {
            var command = new UpdateUserClaimCommand
            {
                UserId = 1,
                ClaimId = new[] { 1, 2 }
            };

            var result = await _updateUserClaimCommandHandler
                .Handle(command, new CancellationToken());

            _userClaimRepository.Verify(x => x.BulkInsert(It.IsAny<int>(), It.IsAny<IEnumerable<UserClaim>>()));
            _userClaimRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Handler_DeleteUserClaim()
        {
            var userClaim = new UserClaim { UserId = 1, ClaimId = 1 };
            _userClaimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<UserClaim, bool>>>()))
                .ReturnsAsync(userClaim);

            var command = new DeleteUserClaimCommand
            {
                Id = 1
            };

            var result = await _deleteUserClaimCommandHandler
                .Handle(command, new CancellationToken());

            _userClaimRepository.Verify(x => x.Delete(It.IsAny<UserClaim>()));
            _userClaimRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Deleted);
        }
    }
}
