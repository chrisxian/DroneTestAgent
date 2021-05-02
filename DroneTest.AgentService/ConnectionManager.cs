using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace DroneTest.AgentService
{
    public class ConnectionManager: IConnectionManager
    {
        private HubConnection myConnection;

        public bool IsConnected { get; private set; }

        public ConnectionManager()
        {
            InitConnection();
        }

        private void InitConnection()
        {
            myConnection = new HubConnectionBuilder()
                           .WithUrl("https://localhost:5001/masterHub")
                           .Build();

            myConnection.Closed += async (error) =>
            {
                //await Task.Delay(new Random().Next(0, 5) * 1000);
                //await myConnection.StartAsync();
                IsConnected = false;
                Console.WriteLine("Connection is down!");
            };

            myConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = user + " says " + message;
                Console.WriteLine($"Received message{encodedMsg}");
            });
        }

        public async Task TryConnect()
        {
            //todo: thread-safe
            if (IsConnected)
            {
                //return; do not return null task!!
                await Task.CompletedTask;
            }
            try
            {
                await myConnection.StartAsync();
                IsConnected = true;
                Console.WriteLine("Connection started");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("exception while connection");
            }
        }
    }
}
