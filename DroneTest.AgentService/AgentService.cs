using System;
using System.Collections.Generic;
using System.Linq;
using DroneTest.AgentService.AgentState;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService
{
    class AgentService : TimedHostedService, IAgentService
    {
        private readonly IEnumerable<IAgentState> myAvailableAgentStates;

        public AgentService(IEnumerable<IAgentState> availableAgentStates, ILogger<AgentService> logger)
            : base(logger)
        {
            myAvailableAgentStates = availableAgentStates;
            SetState(AgentStateType.StartedState);
        }

        public IAgentState CurrentState { get; set; }

        protected override void DoWork(object state)
        {
            //only trigger state handling, do not care about handling result.
            //so no need to change IAgentState.Handle signature to async Task.
            CurrentState.Handle(this);
        }

        public void SetState(AgentStateType requestedStateType)
        {
            var requestedState = myAvailableAgentStates.FirstOrDefault(x => x.StateType == requestedStateType);
            if (requestedState == null)
            {
                var error = $"no available state with requested StateType {requestedStateType}";
                Logger.LogError(error);
                throw new NotSupportedException(error);
            }

            CurrentState = requestedState;
            Logger.LogInformation($"Agent state is set to {requestedStateType}");
        }
    }
}
