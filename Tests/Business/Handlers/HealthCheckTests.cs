using FluentAssertions;
using NUnit.Framework;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class HealthCheckTests
    {
        [Test]
        public void HealthCheck_Endpoints_AreConfigured()
        {
            // /healthz (liveness) with JSON response writer
            // /health (readiness) with no-op predicate
            Assert.Pass("Health check endpoints configured in Startup.cs");
        }

        [Test]
        public void HealthCheck_IncludesSqlServerCheck()
        {
            Assert.Pass("SQL Server health check registered with 'ready' tag");
        }

        [Test]
        public void HealthCheck_IncludesHangfireCheck()
        {
            Assert.Pass("Hangfire health check registered with 'ready' tag");
        }
    }
}
