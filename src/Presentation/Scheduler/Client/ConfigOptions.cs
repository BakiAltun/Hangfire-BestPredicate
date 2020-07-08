using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Client
{
    public static class ConfigOption
    { 

        public class ConnectionStrings
        {
            public string Hangfire { get; set; }
        }
    }
}