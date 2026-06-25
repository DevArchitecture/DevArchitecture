using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.OperationClaims.Commands;
using Business.Handlers.OperationClaims.Queries;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Business.Handlers.OperationClaims.Queries.GetOperationClaimsQuery;
using static Business.Handlers.OperationClaims.Commands.CreateOperationClaimCommand;
using static Business.Handlers.OperationClaims.Commands.UpdateOperationClaimCommand;
using static Business.Handlers.OperationClaims.Commands.DeleteOperationClaimCommand;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class OperationClaimsHandlerTests
    {
        private Mock<IOperationClaimRepository> _operationClaimRepository;

        private GetOperationClaimsQueryHandler _getOperationClaimsQueryHandler;
        private CreateOperationClaimCommandHandler _createOperationClaimCommandHandler;
        private UpdateOperationClaimCommandHandler _updateOperationClaimCommandHandler;
        private DeleteOperationClaimCommandHandler _deleteOperationClaimCommandHandler;

        [SetUp]
        public void Setup()
        {
            _operationClaimRepository = new Mock<IOperationClaimRepository>();
            _getOperationClaimsQueryHandler = new GetOperationClaimsQueryHandler(_operationClaimRepository.Object);
            _createOperationClaimCommandHandler = new CreateOperationClaimCommandHandler(_operationClaimRepository.Object);
            _updateOperationClaimCommandHandler = new UpdateOperationClaimCommandHandler(_operationClaimRepository.Object);
            _deleteOperationClaimCommandHandler = new DeleteOperationClaimCommandHandler(_operationClaimRepository.Object);
        }

        [Test]
        public async Task Handler_GetList()
        {
            var operationClaim = new OperationClaim { Id = 1, Name = "TestClaim" };
            _operationClaimRepository.Setup(x => x.GetListAsync())
                .ReturnsAsync(new List<OperationClaim> { operationClaim }.AsQueryable());

            var result = await _getOperationClaimsQueryHandler
                .Handle(new GetOperationClaimsQuery(), new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(1);
        }

        [Test]
        public async Task Handler_CreateOperationClaim()
        {
            _operationClaimRepository.Setup(x => x.Query()).Returns(new List<OperationClaim>().AsQueryable());

            var command = new CreateOperationClaimCommand
            {
                ClaimName = "NewClaim"
            };

            var result = await _createOperationClaimCommandHandler
                .Handle(command, new CancellationToken());

            _operationClaimRepository.Verify(x => x.Add(It.IsAny<OperationClaim>()));
            _operationClaimRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Handler_UpdateOperationClaim()
        {
            var operationClaim = new OperationClaim { Id = 1, Name = "TestClaim" };
            _operationClaimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<OperationClaim, bool>>>()))
                .ReturnsAsync(operationClaim);

            var command = new UpdateOperationClaimCommand
            {
                Id = 1,
                Alias = "NewAlias",
                Description = "NewDescription"
            };

            var result = await _updateOperationClaimCommandHandler
                .Handle(command, new CancellationToken());

            _operationClaimRepository.Verify(x => x.Update(It.IsAny<OperationClaim>()));
            _operationClaimRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Handler_DeleteOperationClaim()
        {
            var operationClaim = new OperationClaim { Id = 1, Name = "TestClaim" };
            _operationClaimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<OperationClaim, bool>>>()))
                .ReturnsAsync(operationClaim);

            var command = new DeleteOperationClaimCommand
            {
                Id = 1
            };

            var result = await _deleteOperationClaimCommandHandler
                .Handle(command, new CancellationToken());

            _operationClaimRepository.Verify(x => x.Delete(It.IsAny<OperationClaim>()));
            _operationClaimRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Deleted);
        }
    }
}
