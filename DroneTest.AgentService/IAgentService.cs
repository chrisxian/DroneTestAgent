using System;
using System.Collections.Generic;
using System.Text;
using DroneTest.AgentService.AgentState;

namespace DroneTest.AgentService
{
    public interface IAgentService
    {
        IAgentState CurrentState { get; }
    }
}
