using DroneTest.AgentService.AgentState;

namespace DroneTest.AgentService
{
    class Agent : IAgentService
    {
        public IAgentState CurrentState { get; set; }

        public Agent()
        {
            //CurrentState = new StartedState(null);
        }
    }
}
