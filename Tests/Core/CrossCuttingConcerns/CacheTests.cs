using System;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers.Token;

namespace Tests.Core.CrossCuttingConcerns
{
    [TestFixture]
    public class CacheTests : BaseIntegrationTest
    {
        [Test]
        public void CacheTest()
        {
            var cacheManager = new MemoryCacheManager();
            var key = $"TestCache_{Guid.NewGuid()}";
            var first = cacheManager.Get<string>(key);
            first.Should().BeNull();
            cacheManager.IsAdd(key).Should().BeFalse();

            cacheManager.Add(key, "test", 1);

            var second = cacheManager.Get<string>(key);
            second.Should().NotBeNull();
            second.Should().Be("test");
            cacheManager.IsAdd(key).Should().BeTrue();

            cacheManager.Remove(key);
            cacheManager.IsAdd(key).Should().BeFalse();
        }
    }
}