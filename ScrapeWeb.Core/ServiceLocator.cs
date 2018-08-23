using System;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace ScrapeWeb.Core
{
    /// <summary>
    /// Simple dependency wrapper.
    /// </summary>
    public class ServiceLocator : IContainerAccessor, IDisposable
    {
        public IWindsorContainer Container { get; set; }

        private static ServiceLocator _serviceLocator;
        public static ServiceLocator Instance
        {
            get { return _serviceLocator ?? (_serviceLocator = new ServiceLocator()); }
        }

        public ServiceLocator()
        {
            Container = new WindsorContainer();

            // install windsor
            Container.Install(FromAssembly.This());
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}