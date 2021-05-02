using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService
{
    public class MonitorLoop
    {
        private readonly ILogger myLogger;
        private readonly CancellationToken myCancellationToken;

        public MonitorLoop(ILogger<MonitorLoop> logger, IHostApplicationLifetime applicationLifetime)
        {
            myLogger = logger;
            myCancellationToken = applicationLifetime.ApplicationStopping;
        }

        /// <summary>
        /// Run a console user input loop in a background thread
        /// </summary>
        public void StartMonitorLoop()
        {
            myLogger.LogInformation("MonitorAsync Loop is starting.");

            Task.Run(MonitorAsync, myCancellationToken);
        }

        private void MonitorAsync()
        {
            while (!myCancellationToken.IsCancellationRequested)
            {
                var keyStroke = Console.ReadKey();

                if (keyStroke.Key == ConsoleKey.W)
                {
                    // Enqueue a background work item
                    //todo:
                    Console.WriteLine("W key is pressed");
                }
            }
        }
    }
}