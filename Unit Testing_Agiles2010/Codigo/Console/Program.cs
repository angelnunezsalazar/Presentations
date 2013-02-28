using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ClassLibrary;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindsorContainer container = new WindsorContainer();

            container.Register(AllTypes
                                   .FromAssembly(typeof(OrderServices).Assembly)
                                   .Where(x => x.Name.Contains("Services"))
                                   .Configure(c => c.LifeStyle.Transient)
                                   .WithService.FirstInterface());

            var discounDB = container.Resolve<OrderServices>();

        }
    }
}
