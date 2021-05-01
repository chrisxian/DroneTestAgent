using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int myExecutionCount = 0;
        private readonly ILogger<TimedHostedService> myLogger;
        private Timer myTimer;

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            myLogger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            myLogger.LogInformation("Timed Hosted Service running.");

            myTimer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref myExecutionCount);

            myLogger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            myLogger.LogInformation("Timed Hosted Service is stopping.");

            myTimer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            myTimer?.Dispose();
        }
    }
}