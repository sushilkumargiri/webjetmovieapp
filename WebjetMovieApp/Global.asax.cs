using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebjetMovieApp.Installers;
using WebjetMovieApp.Plumbing;
using WebjetMovieAppBL;

namespace WebjetMovieApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WindsorContainer();
            log4net.Config.XmlConfigurator.Configure();
        }

        public static IWindsorContainer container;

        private static void WindsorContainer()
        {
            container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorWebApiControllerActivator(container));
        }
        protected void Application_End()
        {
            container.Dispose();
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Logger.Write(LogLevel.ERROR, exception.Message);
            Response.Clear();

            HttpException httpException = exception as HttpException;
            string action = "";
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "HttpError404";
                        break;
                    case 500:
                        // server error
                        action = "HttpError500";
                        break;
                }
                // clear error on server
                Server.ClearError();
            }
            else
            {
                action = "General";
            }
            Response.Redirect(String.Format("~/Home/Error/{0}/?message={1}", action, exception.Message));
        }
    }
}
