using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService
{
    public class ConnectionManager : IConnectionManager
    {
        private HubConnection myConnection;
        private readonly ILogger<ConnectionManager> myLogger;

        public bool IsConnected { get; private set; }

        public ConnectionManager(ILogger<ConnectionManager> logger)
        {
            myLogger = logger;
            InitConnection();
        }

        private void InitConnection()
        {
            myConnection = new HubConnectionBuilder()
                           .WithUrl("https://localhost:5001/masterHub")
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
