using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;

namespace Scheduler.Client.Jobs
{
    ///<summary>
    /// Günlük iş takip raporu
    ///</summary>
    public class DailyWorkFollowUpReportJob : IJob
    {
        public INotificationBuilder _notificationBuilder { get; }
        public DailyWorkFollowUpReportJob(INotificationBuilder notificationBuilder)
        {
            _notificationBuilder = notificationBuilder;
        }

        public string Schedule => Cron.Minutely();

        public void Execute(string customerName, int customerId)
        {
            Console.WriteLine($"{customerName} {customerId} müşteri için Günlük iş takip raporu başladı");

            _notificationBuilder.SetName("Günlük iş takip raporu")
                                .SetReceiver("hakan.xyzabc@com.tr")
                                .SetReportData("Günlük iş takip raporu verisidir")
                                .BuildAndSend(customerName, customerId);
        }
    }
}