using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService.AgentState
{
    internal class StandbyState : IAgentState
    {
        private readonly ILogger<StandbyState> myLogger;

        public StandbyState(ILogger<StandbyState> logger)
        {
            myLogger = logger;
            StateType = AgentStateType.StandbyState;
        }

        public AgentStateType StateType { get; private set; }

        public void Handle(IAgentService agent)
        {
            myLogger.LogInformation("Handle called");
            //todo: request a test task to execute and enter executing state.

        }
    }
}