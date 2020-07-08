using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduler.Client.Jobs;
using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace Scheduler.Client.App_Start
{
    public class StartupContainer : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(s => s.FromAssemblyContaining<IJob>()
                .SelectAllClasses()
                .InheritedFrom<IJob>()
                .BindAllInterfaces()
                .Configure(x=> x.InTransientScope())
             );

             Kernel.Bind<INotificationBuilder>().To<NotificationBuilder>().InTransientScope();
        }
    }
}