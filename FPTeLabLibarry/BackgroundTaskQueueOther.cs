using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FPTeLabLibarry
{
    public interface IBackgroundTaskQueue: IEnumerable<Action<CancellationToken>>
    {
        void QueueBackgroundWorkItem(Action<CancellationToken> workItem);
        Task<Action<CancellationToken>> DequeueAsync(CancellationToken cancellationToken);
    }
    public class BackgroundTaskQueueOther : IBackgroundTaskQueue
    {
        private ConcurrentQueue<Action<CancellationToken>> _workItems = new ConcurrentQueue<Action<CancellationToken>>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);
        public void QueueBackgroundWorkItem(Action<CancellationToken> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();
        }
        public async Task<Action<CancellationToken>> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
        public IEnumerator<Action<CancellationToken>> GetEnumerator()
        {
            return _workItems.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _workItems.GetEnumerator();
        }
    }
    public class BackgroundTaskQueueResult: BackgroundTaskQueueOther
    {

    }

}
