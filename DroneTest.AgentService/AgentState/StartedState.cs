using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService.AgentState
{
    internal class StartedState : IAgentState
    {
        private readonly IConnectionManager myConnectionManager;
        private readonly ILogger<StartedState> myLogger;

        public StartedState(ILogger<StartedState> logger, IConnectionManager connectionManager)
            => (myLogger, myConnectionManager) = (logger, connectionManager);

        public async void Handle()
        {
            await TryConnect();
            await TryBranchUpdate();
        }

        private async Task TryBranchUpdate()
        {
            myLogger.LogInformation("TryBranchUpdate.");
            //todo:
            await Task.CompletedTask;
        }

        private async Task TryConnect()
        {
            myLogger.LogInformation("TryConnect.");
            await myConnectionManager.TryConnect();
        }
    }
}
