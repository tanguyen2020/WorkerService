using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FPTeLabLibarry
{
    public abstract class HostedServiceOther: IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger _logger;
        protected abstract int Repeat { get; }
        protected abstract void OnExecuteTask(object state);
        public HostedServiceOther(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger?.LogInformation($"{this.GetType().Name} is starting.");
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(Repeat));
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        private void ExecuteTask(object state)
        {
            OnExecuteTask(state);
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
        public T CopyModel<T>()
        {
            return (T)this.MemberwiseClone();
        }
    }
}
