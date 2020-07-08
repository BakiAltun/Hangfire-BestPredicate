using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Client.Jobs
{
    

    public interface IJob
    {
        string Schedule { get; }
        void Execute(string customerName, int customerId);
    }
}