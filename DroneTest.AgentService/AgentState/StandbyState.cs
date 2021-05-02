namespace DroneTest.AgentService.AgentState
{
    internal class StandbyState : IAgentState
    {
        public StandbyState()
        {
            StateType = AgentStateType.StandbyState;
        }

        public AgentStateType StateType { get; private set; }

        public void Handle(IAgentService agent)
        {
            //todo: start connection 
            //todo: start to check if branchupdate needed
        }
    }
}