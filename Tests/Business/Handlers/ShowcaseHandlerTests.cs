using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Showcase.Queries;
using FluentAssertions;
using NUnit.Framework;
using static Business.Handlers.Showcase.Queries.GetShowcaseRowsQuery;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class ShowcaseHandlerTests
    {
        private GetShowcaseRowsQueryHandler _getShowcaseRowsQueryHandler;

        [SetUp]
        public void Setup()
        {
            _getShowcaseRowsQueryHandler = new GetShowcaseRowsQueryHandler();
        }

        [Test]
        public async Task Handler_GetList()
        {
            var result = await _getShowcaseRowsQueryHandler
                .Handle(new GetShowcaseRowsQuery(), new CancellationToken());

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Items.Should().NotBeEmpty();
        }
    }
}
