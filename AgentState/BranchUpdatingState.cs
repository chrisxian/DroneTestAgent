﻿using System;

namespace DroneTest.Agent.AgentState
{
    class BranchUpdatingState : IAgentState
    {
        public void Handle()
        {
            throw new NotImplementedException();
            //todo: observe brancupdate status, on(update success event), enter next state(updated)
        }
    }
}