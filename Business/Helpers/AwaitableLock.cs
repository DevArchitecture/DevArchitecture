using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Helpers
{
    /// <summary>
    /// Since we cannot lock the async await binary with the classic C # lock () clause, we are referring to this solution.
    /// </summary>
    public class AwaitableLock
    {
        private readonly SemaphoreSlim _toLock;

        public AwaitableLock()
        {
            _toLock = new SemaphoreSlim(1, 1);
        }

        public async Task<LockReleaser> Lock(TimeSpan timeout)
        {
            if (await _toLock.WaitAsync(timeout))
            {
                return new LockReleaser(_toLock);
            }

            throw new TimeoutException();
        }

        public struct LockReleaser : IDisposable
        {
            private readonly SemaphoreSlim _toRelease;

            public LockReleaser(SemaphoreSlim toRelease)
            {
                _toRelease = toRelease;
            }

            public void Dispose()
            {
                _toRelease.Release();
            }
        }
    }
}