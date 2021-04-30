namespace DroneTest.Agent.AgentState
{
    class BranchUpdatedState : IAgentState
    {
        private IConnectionManager myConnectionManager;
        private Agent myAgent;
        public BranchUpdatedState(Agent agent, IConnectionManager connectionManager)
        {
            myAgent = agent;
            myConnectionManager = connectionManager;

        }
        public void Handle()
        {
            //todo: check isConnected,
            if (myConnectionManager.IsConnected)
            {
                myAgent.State = new StandbyState();
            }
            else
            {
                myConnectionManager.TryToConnect();
            }
            //if not connected: start reconnect
        }
    }
}
