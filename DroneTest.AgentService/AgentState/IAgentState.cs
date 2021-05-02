namespace DroneTest.AgentService.AgentState
{
    public interface IAgentState
    {
        AgentStateType StateType { get; }

        void Handle(IAgentService agent);
    }
}
