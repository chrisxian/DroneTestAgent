using DroneTest.AgentService.AgentState;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DroneTest.AgentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
            monitorLoop.StartMonitorLoop();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MonitorLoop>();
                    services.AddSingleton<IConnectionManager, ConnectionManager>();
                    services.AddSingleton<IBranchUpdateManager, BranchUpdateManager>();
                    services.AddSingleton<IAgentState, StartedState>();
                    services.AddSingleton<IAgentState, BranchUpdatedState>();
                    services.AddSingleton<IAgentState, StandbyState>();
                    services.AddHostedService<AgentService>();
                });
    }
}
