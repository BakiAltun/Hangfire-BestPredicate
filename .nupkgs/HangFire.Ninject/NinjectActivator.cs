using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire;
using Ninject;
using Ninject.Activation.Blocks;

namespace HangFire.Ninject
{
    /// <summary>
    /// HangFire Job Activator based on Ninject IoC Container.
    /// </summary>
    public class NinjectJobActivator : JobActivator
    {
        private readonly IKernel _kernel;
        private readonly IEnumerable<string> _clients;

        public NinjectJobActivator(IKernel kernel, IEnumerable<string> clients)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");
            if (clients == null) throw new ArgumentNullException("clients");

            _kernel = kernel;
            _clients = clients; 

            //load dlls
            foreach (var client in clients)
            { 
                _kernel.Load(client + "*.dll");
            }
        }

        /// <inheritdoc />
        public override object ActivateJob(Type jobType)
        {
            Console.WriteLine("ActivateJob");
            return _kernel.Get(jobType);
        }

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        { 
            Console.WriteLine("BeginScope");
            return new NinjectScope(_kernel, _clients);
        }
    }
}