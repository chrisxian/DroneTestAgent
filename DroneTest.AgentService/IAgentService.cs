using DroneTest.AgentService.AgentState;

namespace DroneTest.AgentService
{
    public interface IAgentService
    {
        IAgentState CurrentState { get; }

        void SetState(AgentStateType requestedStateType);
    }
}
