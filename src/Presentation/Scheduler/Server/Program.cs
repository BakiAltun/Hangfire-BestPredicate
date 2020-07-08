using System;
using System.ComponentModel;
using System.Reflection;
using Hangfire;
using Hangfire.Server;
using Hangfire.SqlServer;
using Hangfire.Storage;
// using Scheduler.Server.Infrastructure;
using Ninject;
using Microsoft.Extensions.Configuration;
using Scheduler.Server;

namespace Scheduler.Server
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

            Console.ReadLine();
        }

        private static IConfiguration GetConfiguration(string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true);

            return builder.Build();
        }
    }
}
