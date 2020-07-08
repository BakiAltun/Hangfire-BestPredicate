using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;

namespace Scheduler.Client
{
    public class NotificationBuilder : INotificationBuilder
    {
        private string _name;
        public INotificationBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        private string _message;
        public INotificationBuilder SetReportData(string message)
        {
            _message = message;

            return this;
        }

        private string _receiver;
        public INotificationBuilder SetReceiver(string receiver)
        {
            _receiver = receiver;
            return this;
        }


        public void BuildAndSend(string customer, int customerId)
        {
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine($"#{ customerId } - {customer} müşteri için {_name} jobı başladı."));
            var job2Id = BackgroundJob.ContinueJobWith(jobId, () => PreparingReport(customer, customerId));
            var job3Id = BackgroundJob.ContinueJobWith(job2Id, () => CreatingFile(customer, customerId));
            var job4Id = BackgroundJob.ContinueJobWith(job3Id, () => SendNotification(customer, customerId));
        } 

        public void PreparingReport(string customer, int customerId)
        {
            Console.WriteLine(_message);
        }

        public void CreatingFile(string customer, int customerId)
        {
            Console.WriteLine("Creating File");
        }

        public void SendNotification(string customer, int customerId)
        {
            Console.WriteLine("Sending Notification");
        }
    }

    public interface INotificationBuilder
    {
        INotificationBuilder SetName(string name);
        INotificationBuilder SetReportData(string message);
        INotificationBuilder SetReceiver(string receiver);
        void BuildAndSend(string customer, int customerId);
    }
}