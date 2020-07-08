using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Ninject;

namespace Scheduler.Client
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        private IGlobalConfiguration GlobalConfiguration = Hangfire.GlobalConfiguration.Configuration;
        public IKernel Kernel;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices()
        {
            var connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConfigOption.ConnectionStrings>();

            var kernel = ConfigureKernel();
            ConfigureHangfire(kernel, connectionStrings.Hangfire);
            Kernel = kernel;
        }

        public void Configure()
        {

        }

        private IKernel ConfigureKernel()
        {
            //Using Ninject
            IKernel kernel = new StandardKernel();
            var thisAssembly = Assembly.GetExecutingAssembly();// .GetAssembly(typeof(Program));
            kernel.Load(thisAssembly);

            return kernel;
        }

        private void ConfigureHangfire(IKernel kernel, string connectionString)
        {
            GlobalConfiguration
                       .UseColouredConsoleLogProvider()
                       .UseRecommendedSerializerSettings()
                       .UseSqlServerStorage(connectionString);
        }
    }
}