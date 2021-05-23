using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTest.AgentService.Models.Configuration
{
    public class ConnectionConfiguration
    {
        public string ServerHubUrl { get; set; }

        public int MaxRetryCount { get; set; }
    }
}
