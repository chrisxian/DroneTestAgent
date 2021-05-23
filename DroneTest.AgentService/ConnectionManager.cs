using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using DroneTest.AgentService.Models.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DroneTest.AgentService
{
    public class ConnectionManager : IConnectionManager
    {
        private HubConnection myConnection;
        private readonly ILogger<ConnectionManager> myLogger;

        private readonly ConnectionConfiguration myConnectionConfiguration;


        public bool IsConnected { get; private set; }

        public ConnectionManager(ILogger<ConnectionManager> logger, IOptions<ConnectionConfiguration> options)
        {
            myLogger = logger;
            myConnectionConfiguration = options.Value;
            InitConnection();
        }

        private void InitConnection()
        {
            myLogger.LogDebug("Starting connection to {connectionUrl}", myConnectionConfiguration.ServerHubUrl);
            myConnection = new HubConnectionBuilder()
                           .WithUrl(myConnectionConfiguration.ServerHubUrl)
                           .Build();

            myConnection.Closed += async (error) =>
            {
                IsConnected = false;
                myLogger.LogWarning("Connection is down!");
            };

            myConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = user + " says " + message;
                myLogger.LogInformation($"Received message{encodedMsg}");
            });
        }

        public async Task TryConnect()
        {
            //todo: thread-safe
            if (IsConnected)
            {
                myLogger.LogInformation($"connected already");
                return;
            }
            try
            {
                myLogger.LogInformation("Connection starting");
                await myConnection.StartAsync();
                IsConnected = true;
                myLogger.LogInformation("Connection started");
            }
            catch (Exception ex)
            {
                myLogger.LogError($"exception while connection{ex.Message}");
            }
        }
    }
}
