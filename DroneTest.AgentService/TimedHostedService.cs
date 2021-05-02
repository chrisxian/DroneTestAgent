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
                TimeSpan.FromSeconds(10));

            //DoWork of Timer is called in a ThreadPool thread,
            //has to avoid a duplicated State.Handle if previous one is still ongoing.
            //also has to ensure thread-safety in each state implementation!
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            //only trigger state handling, do not care about handling result.
            //so no need to change IAgentState.Handle signature to async Task.
            myAgent.CurrentState.Handle(myAgent);
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