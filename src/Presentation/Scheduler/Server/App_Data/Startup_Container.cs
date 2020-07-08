using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Scheduler.Server.Infrastructure;
using Ninject;
using Ninject.Modules;

namespace Scheduler.Server.AppData
{
    public class StartupContainer : NinjectModule
    {
        public override void Load()
        {
        //     Bind<NinjectScope>().ToMethod(_ =>
        //    {
        //        //    Kernel.Load("C:\\Users\\baki.altun\\source\\repos\\RobotV3\\src\\Presentation\\Scheduler\\Client\\bin\\Debug\\netcoreapp3.1\\*.dll");
        //        return new NinjectScope(Kernel, new ConfigOption.Clients());
        //    });

            // Kernel.Bind(x => x.FromAssembliesInPath(clientPath).SelectAllClasses()
            // .BindAllInterfaces()
            // .Configure(x => x.InTransientScope())
            // );

            // Kernel.Bind(x => .From(new[] {clientPath2 }));

            //   string[] assemblyPatters = { "*.ApplicationCore.dll", "*.Infrastructure.dll", "*.Business.dll", "*.Ldap.dll", "*.Reporting.dll" };
            //     Bind(Type.GetTypeArray(assemblyPatters));
            //     Kernel.Bind(x => x.FromAssembliesMatching(assemblyPatters).SelectAllClasses().BindAllInterfaces());

            //     Bind(typeof(LoggerAdapter<>)).To(typeof(IAppLogger<>));
            //     Bind(typeof(EfRepository<>)).To(typeof(IAsyncRepository<>));
            //     Bind(typeof(EntityLocalizer<>)).To(typeof(IEntityLocalizer<>));

            //     string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            //     Rebind(typeof(Serilog.ILogger)).ToConstant(LoggerExtension.GetLogger(connectionString));

            //     Kernel.Bind(s => s.FromAssemblyContaining<IJob>()
            //         .SelectAllClasses()
            //         .InheritedFrom<IJob>()
            //         .BindAllInterfaces()
            //      );
        }
    }
}