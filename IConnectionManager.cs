using System;
using System.Collections.Generic;
using System.Text;

namespace DroneTest.Agent
{
    public interface IConnectionManager
    {
        bool IsConnected { get; }

        void TryToConnect();
    }
}
