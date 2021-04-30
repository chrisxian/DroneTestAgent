using System;

namespace DroneTest.Agent.AgentState
{
    internal class StartedState : IAgentState
    {
        private readonly IConnectionManager myConnectionManager;

        public StartedState(IConnectionManager connectionManager)
        {
            myConnectionManager = connectionManager;
        }
        public void Handle()
        {
            ConnectIfNeeded();
            BranchUpdateIfNeeded();
        }

        private void BranchUpdateIfNeeded()
        {
            throw new NotImplementedException();
        }

        private void ConnectIfNeeded()
        {
            myConnectionManager.TryToConnect();
        }
    }
}
