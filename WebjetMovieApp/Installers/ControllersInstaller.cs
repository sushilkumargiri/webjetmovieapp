using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Web.Mvc;
using WebjetMovieAppBL;
using System.Web.Http.Controllers;

namespace WebjetMovieApp.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Bind all implementation of interfaces using fluent interface.
        /// True separation of concern can be achieved and detachable applications can be developed.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="store"></param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Note that singleton object can also be created here. 
            //There are some other options also in windson castle IOC for life cycle of objects
            container.Register(Classes.FromThisAssembly()
                                .BasedOn<IController>()//MVC
                                .LifestyleTransient());

            container.Register(Classes.
                FromThisAssembly().
                BasedOn<IHttpController>(). //Web API
                If(c => c.Name.EndsWith("Controller")).
                LifestyleTransient());

            container.Register(Component.For<IAPICredentials>().ImplementedBy<APICredentials>()
                                .LifestyleTransient());

            container.Register(Component.For<IMovie>().ImplementedBy<MovieBL>()
                                .LifestyleTransient());
        }
    }
}