using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using WebjetMovieApp.Plumbing;

namespace WebjetMovieApp.Tests
{
    public static class Windsor
    {
        public static IWindsorContainer container;
        public static void WindsorContainer()
        {
            container = new WindsorContainer().Install(FromAssembly.InThisApplication());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorWebApiControllerActivator(container));
        }
    }
}
