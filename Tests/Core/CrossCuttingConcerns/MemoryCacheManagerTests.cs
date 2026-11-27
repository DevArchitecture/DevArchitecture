using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.Results;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;

namespace Tests.Core.CrossCuttingConcerns
{
    [TestFixture]
    public class MemoryCacheManagerTests
    {
        private MemoryCache _cache;
        private MemoryCacheManager _cacheManager;

        [SetUp]
        public void SetUp()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _cacheManager = new MemoryCacheManager(_cache);
        }

        [TearDown]
        public void TearDown()
        {
            _cache.Dispose();
        }

        [Test]
        public async Task Get_ReturnsCachedResultAsDeclaredTaskType()
        {
            var key = $"GetStrings_{Guid.NewGuid()}";
            var result = new SuccessDataResult<List<string>>(new List<string> { "a" });

            _cacheManager.Add(key, Task.FromResult<IDataResult<List<string>>>(result), 1, typeof(IDataResult<List<string>>));

            var cached = (Task<IDataResult<List<string>>>)_cacheManager.Get(key, typeof(IDataResult<List<string>>));

            (await cached).Data.Should().ContainSingle().Which.Should().Be("a");
        }

        [Test]
        public void RemoveByPattern_RemovesOnlyMatchingKeys()
        {
            var groupKey = $"Business.Handlers.Groups.Queries.GetGroupsQuery.{Guid.NewGuid()}";
            var userKey = $"Business.Handlers.Users.Queries.GetUsersQuery.{Guid.NewGuid()}";
            _cacheManager.Add(groupKey, "groups", 1);
            _cacheManager.Add(userKey, "users", 1);

            _cacheManager.RemoveByPattern("GetGroup");

            _cacheManager.IsAdd(groupKey).Should().BeFalse();
            _cacheManager.IsAdd(userKey).Should().BeTrue();
        }

        [Test]
        public void RemoveByPattern_UnmatchedPattern_KeepsAllKeys()
        {
            var key = $"GetGroups_{Guid.NewGuid()}";
            _cacheManager.Add(key, "groups", 1);

            _cacheManager.RemoveByPattern("GetLanguage");

            _cacheManager.IsAdd(key).Should().BeTrue();
        }
    }
}
