using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService.AgentState
{
    internal class StartedState : IAgentState
    {
        private readonly ILogger<StartedState> myLogger;
        private readonly IBranchUpdateManager myBranchUpdateManager;
        private enum RsfaState
        {
            BranchUpdateNeeded,
            BranchUpdating,
            BranchUpdated
        }

        private RsfaState myRsfaState = RsfaState.BranchUpdateNeeded;

        public StartedState(ILogger<StartedState> logger, IBranchUpdateManager branchUpdateManager)
        {
            myLogger = logger;
            myBranchUpdateManager = branchUpdateManager;
            StateType = AgentStateType.StartedState;
        }

        public AgentStateType StateType { get; private set; }

        public void Handle(IAgentService agent)
        {
            CheckBranchUpdate(agent);
        }

        private async void CheckBranchUpdate(IAgentService agent)
        {
            //todo: thread safe
            myLogger.LogInformation("CheckBranchUpdate.");
            //await myBranchUpdateManager.CheckBranchUpdate();
            if (myRsfaState != RsfaState.BranchUpdateNeeded)
            {
                myLogger.LogDebug("BranchUpdate not needed or is ongoing");
                return;
            }

            if (myBranchUpdateManager.CheckBranchUpdateNeeded())
            {
                myLogger.LogInformation("Trigger BranchUpdate ");
                myRsfaState = RsfaState.BranchUpdating;
                await myBranchUpdateManager.BranchUpdate();

                myLogger.LogInformation("BranchUpdate finished");
                myRsfaState = RsfaState.BranchUpdated;
                agent.SetState(AgentStateType.BranchUpdatedState);

                //branchUpdate is actually performed in BranchUpdatingState.
                //so to enjoy async await pattern.
            }
            else
            {
                myRsfaState = RsfaState.BranchUpdated;
                //to branchUpdated state.
                agent.SetState(AgentStateType.BranchUpdatedState);
            }
        }
    }
}
