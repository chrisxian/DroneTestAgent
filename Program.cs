using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR.Client;

namespace DroneTest.Agent
{
    class Program
    {
        private static HubConnection myConnection;

        static void Main( string[] args )
        {
            InitConnection();

            TryToConnect();

            ProcessUserMessageLoop();

            Console.WriteLine( "exiting" );
        }

        private static void ProcessUserMessageLoop()
        {
            Console.WriteLine( "Please specify command for new operation: \n status: print agent status \n connect: try to connect to configured server \n exit: press 'x'" );

            string newCommand = Console.ReadLine();
            while( newCommand != "x" )
            {
                if( newCommand == "s" ) //status
                {
                    PrintAgentStatus();
                }
                else if( newCommand == "c" ) //connect
                {
                    TryToConnect();
                }

                newCommand = Console.ReadLine();
            }
        }

        private static void PrintAgentStatus()
        {

        }

        private static void InitConnection()
        {
            myConnection = new HubConnectionBuilder()
                           .WithUrl( "https://localhost:5001/masterHub" )
                           .Build();

            myConnection.Closed += async ( error ) =>
            {
                await Task.Delay( new Random().Next( 0, 5 ) * 1000 );
                await myConnection.StartAsync();
            };

            myConnection.On<string, string>( "ReceiveMessage", ( user, message ) =>
            {
                var encodedMsg = user + " says " + message;
                Console.WriteLine( $"Received message{encodedMsg}" );
            } );
        }

        /// <summary>
        /// max retry count 10.
        /// </summary>
        private static async void TryToConnect()
        {
            try
            {
                await myConnection.StartAsync();
                Console.WriteLine( "Connection started" );
            }
            catch( Exception ex )
            {
                Console.Error.WriteLine( "exception while connection" );
            }
        }
    }
}