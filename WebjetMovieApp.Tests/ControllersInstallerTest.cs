using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.Windsor;
using Castle.MicroKernel;
using System.Web.Mvc;
using Castle.Core;
using Castle.Core.Internal;
using WebjetMovieApp.Controllers;
using WebjetMovieApp.Installers;

namespace Controllers.Tests
{
    //unit tests using WindsorContainer and Visual Studio Unit Test Tool
    [TestClass]
    public class ControllersInstallerTest
    {
        private IWindsorContainer containerWithControllers;
        public ControllersInstallerTest()
        {
            containerWithControllers = new WindsorContainer()
                        .Install(new ControllersInstaller());
        }

        [TestMethod]
        public void All_Handlers_implement_IController()
        {
            var allHandlers = GetAllHandlers(containerWithControllers);
            var controllerHandlers = GetHandlersFor(typeof(IController), containerWithControllers);

            Assert.IsNotNull(allHandlers);
            //Assert.AreEqual(allHandlers, controllerHandlers);

            CollectionAssert.AreEquivalent(allHandlers, controllerHandlers);
        }
        private IHandler[] GetAllHandlers(IWindsorContainer container)
        {
            return GetHandlersFor(typeof(object), container);
        }

        private IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }
        [TestMethod]
        public void All_controllers_are_registered()
        {
            // Is<TType> is an helper, extension method from Windsor in the Castle.Core.Internal namespace
            // which behaves like 'is' keyword in C# but at a Type, not instance level
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
            var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
            //Assert.AreEqual(allControllers, registeredControllers);
            CollectionAssert.AreEquivalent(allControllers, registeredControllers);
        }
        private Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
        {
            return GetHandlersFor(type, container)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(HomeController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }
        [TestMethod]
        public void All_and_only_controllers_have_Controllers_suffix()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
            //Assert.AreEqual(allControllers, registeredControllers);
            CollectionAssert.AreEquivalent(allControllers, registeredControllers);
        }

        [TestMethod]
        public void All_and_only_controllers_live_in_Controllers_namespace()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Controllers"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
            //Assert.AreEqual(allControllers, registeredControllers);
            CollectionAssert.AreEquivalent(allControllers, registeredControllers);
        }
        [TestMethod]
        public void All_controllers_are_transient()
        {
            var nonTransientControllers = GetHandlersFor(typeof(IController), containerWithControllers)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient);

            Assert.AreEqual(nonTransientControllers.ToList().Count, 0);
        }

        [TestMethod]
        public void All_controllers_expose_themselves_as_service()
        {
            var controllersWithWrongName = GetHandlersFor(typeof(IController), containerWithControllers)
                .Where(controller => controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation);

            Assert.AreEqual(controllersWithWrongName.ToList().Count, 0);
        }
    }
}
