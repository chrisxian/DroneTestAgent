using System;
using System.IO;
using System.Xml.Xsl;
using DroneTest.AgentService.AgentState;
using DroneTest.AgentService.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace DroneTest.AgentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger(); // <-- Change this line!

            try
            {
                using IHost host = CreateHostBuilder(args).Build();

                var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
                monitorLoop.StartMonitorLoop();
                host.Run();
            }
            catch (Exception ex)
            {
                //try..catch ensure any start-up issues with your app are appropriately logged.
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();//ensure all logs are flushed before process exit.
            }
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
                    services.Configure<ConnectionConfiguration>(hostContext.Configuration.GetSection(nameof(ConnectionConfiguration)));
                }).UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console());
    }
}
