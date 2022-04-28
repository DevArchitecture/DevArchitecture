using NUnit.Framework;

namespace Tests.Business.Handlers;

[TestFixture]
public class LogHandlerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public Task Log_GetQuery_Success()
    {
        return Task.CompletedTask;
    }
}
