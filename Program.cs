using System;

namespace DroneTest.Agent
{
    class Program
    {
        private static ConnectionManager myConnectionManager;
        private static Agent myAgent;

        static void Main(string[] args)
        {
            myConnectionManager = new ConnectionManager();

            myAgent = new Agent();

            ProcessUserMessageLoop();

            myAgent.Dispose();
            Console.WriteLine("exiting");
        }

        private static void ProcessUserMessageLoop()
        {
            Console.WriteLine("Please specify command for new operation: \n status: print agent status \n connect: try to connect to configured server \n exit: press 'x'");

            string newCommand = Console.ReadLine();
            while (newCommand != "x")
            {
                if (newCommand == "s") //status
                {
                    PrintAgentStatus();
                }
                else if (newCommand == "c") //connect
                {
                    myConnectionManager.TryToConnect();
                }

                newCommand = Console.ReadLine();
            }
        }

        private static void PrintAgentStatus()
        {

        }
    }
}