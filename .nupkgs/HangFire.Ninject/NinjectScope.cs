using System;
using Hangfire;
using Ninject;
using Ninject.Activation.Caching;
using System.Collections.Generic;

namespace HangFire.Ninject
{

    public class NinjectScope : JobActivatorScope
    {
        private readonly IKernel _kernel;

        public NinjectScope(IKernel kernel, IEnumerable<string> clients)
        {
            _kernel = kernel;
        }

        public override object Resolve(Type type)
        {
            Console.WriteLine("Resolve");
            return _kernel.Get(type);
        }

        public override void DisposeScope()
        {
            Console.WriteLine("DisposeScope");

            // _kernel.Unload("Scheduler.Client.App_Start.StartupContainer");

            _kernel.Components.Get<ICache>().Clear(JobActivatorScope.Current);
        }
    }

}