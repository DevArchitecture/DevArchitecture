using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.GroupClaims.Commands;
using Business.Handlers.GroupClaims.Queries;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Business.Handlers.GroupClaims.Queries.GetGroupClaimsQuery;
using static Business.Handlers.GroupClaims.Commands.CreateGroupClaimCommand;
using static Business.Handlers.GroupClaims.Commands.UpdateGroupClaimCommand;
using static Business.Handlers.GroupClaims.Commands.DeleteGroupClaimCommand;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class GroupClaimsHandlerTests
    {
        private Mock<IGroupClaimRepository> _groupClaimRepository;
        private Mock<IOperationClaimRepository> _operationClaimRepository;

        private GetGroupClaimsQueryHandler _getGroupClaimsQueryHandler;
        private CreateGroupClaimCommandHandler _createGroupClaimCommandHandler;
        private UpdateGroupClaimCommandHandler _updateGroupClaimCommandHandler;
        private DeleteGroupClaimCommandHandler _deleteGroupClaimCommandHandler;

        [SetUp]
        public void Setup()
        {
            _groupClaimRepository = new Mock<IGroupClaimRepository>();
            _operationClaimRepository = new Mock<IOperationClaimRepository>();
            _getGroupClaimsQueryHandler = new GetGroupClaimsQueryHandler(_groupClaimRepository.Object);
            _createGroupClaimCommandHandler = new CreateGroupClaimCommandHandler(_operationClaimRepository.Object);
            _updateGroupClaimCommandHandler = new UpdateGroupClaimCommandHandler(_groupClaimRepository.Object);
            _deleteGroupClaimCommandHandler = new DeleteGroupClaimCommandHandler(_groupClaimRepository.Object);
        }

        [Test]
        public async Task Handler_GetList()
        {
            var groupClaim = new GroupClaim { GroupId = 1, ClaimId = 1 };
            _groupClaimRepository.Setup(x => x.GetListAsync())
                .ReturnsAsync(new List<GroupClaim> { groupClaim }.AsQueryable());

            var result = await _getGroupClaimsQueryHandler
                .Handle(new GetGroupClaimsQuery(), new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(1);
        }

        [Test]
        public async Task Handler_CreateGroupClaim()
        {
            var command = new CreateGroupClaimCommand
            {
                ClaimName = "NewClaim"
            };

            var result = await _createGroupClaimCommandHandler
                .Handle(command, new CancellationToken());

            _operationClaimRepository.Verify(x => x.Add(It.IsAny<OperationClaim>()));
            _operationClaimRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Handler_UpdateGroupClaim()
        {
            var command = new UpdateGroupClaimCommand
            {
                GroupId = 1,
                ClaimIds = new[] { 1, 2 }
            };

            var result = await _updateGroupClaimCommandHandler
                .Handle(command, new CancellationToken());

            _groupClaimRepository.Verify(x => x.BulkInsert(It.IsAny<int>(), It.IsAny<IEnumerable<GroupClaim>>()));
            _groupClaimRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Handler_DeleteGroupClaim()
        {
            var groupClaim = new GroupClaim { GroupId = 1, ClaimId = 1 };
            _groupClaimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<GroupClaim, bool>>>()))
                .ReturnsAsync(groupClaim);

            var command = new DeleteGroupClaimCommand
            {
                Id = 1
            };

            var result = await _deleteGroupClaimCommandHandler
                .Handle(command, new CancellationToken());

            _groupClaimRepository.Verify(x => x.Delete(It.IsAny<GroupClaim>()));
            _groupClaimRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Deleted);
        }
    }
}
