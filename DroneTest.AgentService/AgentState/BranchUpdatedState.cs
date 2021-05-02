namespace DroneTest.AgentService.AgentState
{
    internal class BranchUpdatedState : IAgentState
    {
        private readonly IConnectionManager myConnectionManager;
        private readonly Agent myAgent;

        public BranchUpdatedState(Agent agent, IConnectionManager connectionManager)
            => (myAgent, myConnectionManager) = (agent, connectionManager);

        public void Handle()
        {
            //todo: check isConnected,
            if (myConnectionManager.IsConnected)
            {
                myAgent.CurrentState = new StandbyState();
            }
            else
            {
                myConnectionManager.TryConnect();
            }
            //if not connected: start reconnect
        }
    }
}
