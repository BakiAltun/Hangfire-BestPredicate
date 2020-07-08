using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using Ninject;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Scheduler.Client.Jobs;

namespace Scheduler.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = GetConfiguration(environmentName);
            var statup = new Startup(configuration);

            statup.ConfigureServices();
            statup.Configure();

            Run(statup.Kernel);

            Console.WriteLine("5 dk sonra uygulama kapanacak.");
            Thread.Sleep(TimeSpan.FromMinutes(5));
        }

        private static IConfiguration GetConfiguration(string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true);

            return builder.Build();
        }


        private static void Run(IKernel kernel)
        {
            //get only defined customers
            var customers = new Dictionary<string, int> { { "Demsa", 1 }, { "Akpa", 2 } };
            var jobs = kernel.GetAll<IJob>();

            foreach (var customer in customers)
            {
                foreach (var job in jobs)
                {
                    string jobName = job.GetType().Name + "-" + customer.Key + "-" + customer.Value;

                    RecurringJob.AddOrUpdate(jobName, () => job.Execute(customer.Key, customer.Value), job.Schedule);
                }
            }
        }
    }
}