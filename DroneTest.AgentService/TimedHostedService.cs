using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> myLogger;
        private readonly IAgentService myAgent;
        private Timer myTimer;

        public TimedHostedService(IAgentService agent, ILogger<TimedHostedService> logger)
        => (myAgent, myLogger) = (agent, logger);


        public Task StartAsync(CancellationToken stoppingToken)
        {
            myLogger.LogInformation("Timed Hosted Service running.");

            myTimer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            //triggered in a background thread, has to ensure thread-safety in each state implementation!
            myAgent.CurrentState.Handle();
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