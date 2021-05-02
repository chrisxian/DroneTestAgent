using System;
using System.Collections.Generic;
using System.Linq;
using DroneTest.AgentService.AgentState;
using Microsoft.Extensions.Logging;

namespace DroneTest.AgentService
{
    class Agent : IAgentService
    {
        private readonly IEnumerable<IAgentState> myAvailableAgentStates;
        private readonly ILogger<Agent> myLogger;

        public Agent(IEnumerable<IAgentState> availableAgentStates, ILogger<Agent> logger)
        {
            myAvailableAgentStates = availableAgentStates;
            myLogger = logger;
            SetState(AgentStateType.StartedState);
        }

        public IAgentState CurrentState { get; set; }

        public void SetState(AgentStateType requestedStateType)
        {
            var requestedState = myAvailableAgentStates.FirstOrDefault(x => x.StateType == requestedStateType);
            if (requestedState == null)
            {
                var error = $"no available state with requested StateType {requestedStateType}";
                myLogger.LogError(error);
                throw new NotSupportedException(error);
            }

            CurrentState = requestedState;
            myLogger.LogInformation($"Agent state is set to {requestedStateType}");
        }
    }
}
