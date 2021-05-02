using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService
{
    class BranchUpdateManager : IBranchUpdateManager
    {
        private ILogger<BranchUpdateManager> myLogger;

        public BranchUpdateManager(ILogger<BranchUpdateManager> logger)
        {
            myLogger = logger;
        }

        public bool CheckBranchUpdateNeeded()
        {
            return true;
        }

        public async Task BranchUpdate()
        {
            if (!CheckBranchUpdateNeeded())
            {
                await Task.CompletedTask;
            }

            //todo: 
            //delay 30s
            await Task.Delay(1000 * 30);
            myLogger.LogInformation("Sorry for the delay..., Mock branchUpdate is finished\n");

        }
    }
}
