using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService
{
    public abstract class TimedHostedService : IHostedService, IDisposable
    {
        protected readonly ILogger<TimedHostedService> Logger;
        private Timer myTimer;

        protected TimedHostedService(ILogger<TimedHostedService> logger)
            => Logger = logger;


        public Task StartAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Timed Hosted Service running.");

            myTimer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            //DoWork of Timer is called in a ThreadPool thread,
            //has to avoid a duplicated State.Handle if previous one is still ongoing.
            //also has to ensure thread-safety in each state implementation!
            return Task.CompletedTask;
        }

        protected abstract void DoWork(object state);

        public Task StopAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Timed Hosted Service is stopping.");

            myTimer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            myTimer?.Dispose();
        }
    }
}