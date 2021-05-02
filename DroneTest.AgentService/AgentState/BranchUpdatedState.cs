using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService.AgentState
{
    internal class BranchUpdatedState : IAgentState
    {
        private readonly ILogger myLogger;
        private readonly IConnectionManager myConnectionManager;
        private readonly CancellationToken myCancellationToken;

        public BranchUpdatedState(IConnectionManager connectionManager, ILogger<BranchUpdatedState> logger, IHostApplicationLifetime applicationLifetime)
        {
            StateType = AgentStateType.BranchUpdatedState;

            myConnectionManager = connectionManager;
            myLogger = logger;
            myCancellationToken = applicationLifetime.ApplicationStopping;
        }

        public AgentStateType StateType { get; private set; }

        public async void Handle(IAgentService agent)
        {
            //thread safe: how to avoid duplicated TryConnect call.
            //assumption: TimedHostedService interval is longer than TryConnect,
            //otherwise have to implement in a thread safe way checking is tryConnect ongoing.
            await myConnectionManager.TryConnect();

            if (myConnectionManager.IsConnected)
            {
                agent.SetState(AgentStateType.StandbyState);
            }
            else
            {
                //stays in BranchUpdatedStated until next State.Handle by TimedHostedService.
            }
        }
    }
}
