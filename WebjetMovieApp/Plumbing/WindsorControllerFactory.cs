using System;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel;
using System.Web.Routing;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace WebjetMovieApp.Plumbing
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }
        /// <summary>
        /// Garbage collect object after use
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }
        /// <summary>
        /// Get instance of object
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)kernel.Resolve(controllerType);
        }
    }
    public class WindsorWebApiControllerActivator : IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;

        public WindsorWebApiControllerActivator(IWindsorContainer container)
        {
            _container = container;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller =
                (IHttpController)this._container.Resolve(controllerType);

            request.RegisterForDispose(
                new Release(
                    () => this._container.Release(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                _release = release;
            }

            public void Dispose()
            {
                _release();
            }
        }
    }
}