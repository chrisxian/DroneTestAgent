using System;
using System.IO;
using DroneTest.AgentService.AgentState;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DroneTest.AgentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .Build();

            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();

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

                }).UseSerilog();
        //}).ConfigureLogging(builder =>
        //    builder.AddSimpleConsole(options =>
        //    {
        //        options.IncludeScopes = true;
        //        options.SingleLine = true;
        //        options.TimestampFormat = "hh:mm:ss ";
        //    }));
    }
}
