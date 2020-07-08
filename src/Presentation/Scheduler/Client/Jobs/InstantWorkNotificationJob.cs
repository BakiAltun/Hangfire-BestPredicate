using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;

namespace Scheduler.Client.Jobs
{
    ///<summary>
    /// Anlık iş bildirimi
    ///</summary>
    public class InstantWorkNotificationJob : IJob
    {
        public INotificationBuilder _notificationBuilder { get; }
        public InstantWorkNotificationJob(INotificationBuilder notificationBuilder)
        {
            _notificationBuilder = notificationBuilder;
        }

        public string Schedule => Cron.Minutely();

        public void Execute(string customerName, int customerId)
        {
            Console.WriteLine($"{customerName} {customerId} müşteri için anlık iş bildirim başladı");

            _notificationBuilder
                .SetName("anlık iş bildirim")
                .SetReceiver("baki.altun@com.tr")
                .SetReportData("Anlık iş bildirim verisidir")
                .BuildAndSend(customerName, customerId);
        }
    }
}