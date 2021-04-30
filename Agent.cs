using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using DroneTest.Agent.AgentState;

namespace DroneTest.Agent
{
    class Agent : IDisposable
    {
        public IAgentState State { get; set; }
        private readonly Timer myTimeTrigger;

        public Agent()
        {
            State = new StartedState(null);

            myTimeTrigger = new Timer(1000);
            myTimeTrigger.Elapsed += OnTimedEvent;
            myTimeTrigger.Enabled = true;
            myTimeTrigger.Start();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            State.Handle();
        }

        public void Dispose()
        {
            myTimeTrigger?.Dispose();
        }
    }
}
