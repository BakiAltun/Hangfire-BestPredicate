using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HangFire.Ninject;
// using Scheduler.Server.Infrastructure;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Ninject;

namespace Scheduler.Server
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        private IGlobalConfiguration GlobalConfiguration = Hangfire.GlobalConfiguration.Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices()
        {
            var clients = Configuration.GetSection("Clients").Get<ConfigOption.Clients>();
            var connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConfigOption.ConnectionStrings>();

            var kernel = ConfigureKernel();
            ConfigureHangfire(kernel, clients, connectionStrings);
        }

        public void Configure()
        {
            var _ = new BackgroundJobServer
            {


            };

            // using (var server = new BackgroundJobServer
            // {

            // })
            // {
            //     Console.WriteLine("Server ayakta!");
            // }
        }

        private IKernel ConfigureKernel()
        {
            //Using Ninject
            IKernel kernel = new StandardKernel();
            var thisAssembly = Assembly.GetExecutingAssembly();// .GetAssembly(typeof(Program));
            kernel.Load(thisAssembly);

            return kernel;
        }

        private void ConfigureHangfire(IKernel kernel, ConfigOption.Clients clients, ConfigOption.ConnectionStrings connectionStrings)
        {
            GlobalConfiguration
                       .UseColouredConsoleLogProvider()
                       .UseRecommendedSerializerSettings()
                       .UseActivator(new NinjectJobActivator(kernel, new[] { clients.Optimus }))
                       .UseSqlServerStorage(connectionStrings.Hangfire, new SqlServerStorageOptions
                       {
                           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                           QueuePollInterval = TimeSpan.Zero,
                           UseRecommendedIsolationLevel = true,
                           UsePageLocksOnDequeue = true,
                           DisableGlobalLocks = true
                       });
        }
    }
}