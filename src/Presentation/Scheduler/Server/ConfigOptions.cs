using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Server
{
    public static class ConfigOption
    {
        public class Clients
        { 
            public string Optimus { get; set; }
        }

        public class ConnectionStrings
        {
            public string Hangfire { get; set; }
        }
    }
}