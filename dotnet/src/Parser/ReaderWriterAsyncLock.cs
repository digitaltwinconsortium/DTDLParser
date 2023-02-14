namespace DTDLParser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a lock that is used to manage access to a resource, allowing multiple threads for reading or exclusive access for writing.
    /// Similar to the <c>ReaderWriterLockSlim</c> class but provides Async methods for lock entry in place of TryEnter methods.
    /// </summary>
    internal class ReaderWriterAsyncLock : IDisposable
    {
        private int maxReadConcurrency;
        private bool stronglyFavorReaders;
        private SemaphoreSlim readSemaphore;
        private SemaphoreSlim writeSemaphore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterAsyncLock"/> class.
        /// </summary>
        /// <param name="maxReadConcurrency">The maximum count of read locks that may be held concurrently.</param>
        /// <param name="stronglyFavorReaders">True if continuous read requests are permitted to starve a pending write request.</param>
        internal ReaderWriterAsyncLock(int maxReadConcurrency, bool stronglyFavorReaders = false)
        {
            this.maxReadConcurrency = maxReadConcurrency;
            this.stronglyFavorReaders = stronglyFavorReaders;
            this.readSemaphore = new SemaphoreSlim(maxReadConcurrency, maxReadConcurrency);
            this.writeSemaphore = new SemaphoreSlim(1, 1);
        }

        /// <summary>
        /// Releases resources used by the current instance of the <see cref="ReaderWriterAsyncLock"/> class.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.readSemaphore.Dispose();
            this.writeSemaphore.Dispose();
        }

        /// <summary>
        /// Synchronously enter the lock in read mode.
        /// </summary>
        internal void EnterReadLock()
        {
            // To keep continuous read requests from starving a pending write, have the reader wait for the write semaphore to give the pending writer an opportunity.
            // As soon as the write semaphore is acquired, it can be released.  This does not protect anything in the read path; it just shifts priority.
            if (!this.stronglyFavorReaders && this.writeSemaphore.CurrentCount == 0)
            {
                this.writeSemaphore.Wait();
                this.writeSemaphore.Release();
            }

            this.readSemaphore.Wait();
        }

        /// <summary>
        /// Synchronously enter the lock in write mode.
        /// </summary>
        internal void EnterWriteLock()
        {
            // Acquire the write lock so that only one pending writer at a time attempts to acquire all of the read locks.
            this.writeSemaphore.Wait();

            // Acquire all of the read locks to prevent any readers from simultaneously accessing the resource.  Semantics are single writer OR multiple readers.
            for (int i = 0; i < this.maxReadConcurrency; ++i)
            {
                this.readSemaphore.Wait();
            }
        }

        /// <summary>
        /// Exits read mode.
        /// </summary>
        internal void ExitReadLock()
        {
            this.readSemaphore.Release();
        }

        /// <summary>
        /// Exits write mode.
        /// </summary>
        internal void ExitWriteLock()
        {
            // No for loop is needed here because SemaphoreSlim conveniently allows multiple releases in a single method call.  Unfortunate it does not do this for wait.
            this.readSemaphore.Release(this.maxReadConcurrency);
            this.writeSemaphore.Release();
        }

        /// <summary>
        /// Asynchronously enter the lock in read mode.
        /// </summary>
        /// <returns>A <c>Task</c> that completes when the lock is entered in read mode.</returns>
        internal async Task EnterReadLockAsync()
        {
            // To keep continuous read requests from starving a pending write, have the reader wait for the write semaphore to give the pending writer an opportunity.
            // As soon as the write semaphore is acquired, it can be released.  This does not protect anything in the read path; it just shifts priority.
            if (!this.stronglyFavorReaders && this.writeSemaphore.CurrentCount == 0)
            {
                await this.writeSemaphore.WaitAsync().ConfigureAwait(false);
                this.writeSemaphore.Release();
            }

            await this.readSemaphore.WaitAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously enter the lock in write mode.
        /// </summary>
        /// <returns>A <c>Task</c> that completes when the lock is entered in write mode.</returns>
        internal async Task EnterWriteLockAsync()
        {
            // Acquire the write lock so that only one pending writer at a time attempts to acquire all of the read locks.
            await this.writeSemaphore.WaitAsync().ConfigureAwait(false);

            // Acquire all of the read locks to prevent any readers from simultaneously accessing the resource.  Semantics are single writer OR multiple readers.
            for (int i = 0; i < this.maxReadConcurrency; ++i)
            {
                await this.readSemaphore.WaitAsync().ConfigureAwait(false);
            }
        }
    }
}
