using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;
using Utility;
using Lighter.BaseService.Interface;
using Lighter.ServiceManager.Composition;

namespace Lighter.ServiceManager
{
    public class ServiceHostBootstrapper : Bootstrapper
    {

        //protected AggregateCatalog AggregateCatalog { get; set; }

        protected DirectoryCatalog _directoryCatalog { get; set; }

        public CompositionContainer _container { get; set; }

        protected ICompositionContainerFactory _factory;

        public override void Run(bool runWithDefaultConfiguration)
        {
            this.Logger = this.CreateLogger();
            if (this.Logger == null)
                throw new InvalidOperationException("No Logger Facade Found!");

            this.Logger.Log("Logger created in service", Category.Debug, Priority.Low);

            //this.Logger.Log("Creating Aggregate Catalog For MEF Service", Category.Debug, Priority.Low);
            //this.AggregateCatalog = this.CreateAggregateCatalog();

            //this.Logger.Log("Configuring Aggregate Catalog For MEF Service", Category.Debug, Priority.Low);
            //this.ConfigureAggregateCatalog();

            //this.RegisterDefaultTypesIfMissing();

            this.Logger.Log("Creating Aggregate Catalog For MEF Service", Category.Debug, Priority.Low);
            this.CreateContainerFactory();

            //this.Logger.Log("Configuring Aggregate Catalog For MEF Service", Category.Debug, Priority.Low);
            //this.ConfigureDirectoryCatalog();

            //this.RegisterDefaultTypesIfMissing();

            this.Logger.Log("Creating Mef Container", Category.Debug, Priority.Low);
            this._container = this.CreateContainer();

            this.Logger.Log("Configuring Mef Container", Category.Debug, Priority.Low);
            this.ConfigureContainer();

            this.Logger.Log("Configuring Service Locator Singleton for Service", Category.Debug, Priority.Low);
            this.ConfigureServiceLocator();

            this.Logger.Log("Registering Framework Exception Types", Category.Debug, Priority.Low);
            this.RegisterFrameworkExceptionTypes();

            this.Logger.Log("ServiceHostBootstrapper Sequence Completed", Category.Debug, Priority.Low);
        }

        protected override void ConfigureServiceLocator()
        {
            IServiceLocator serviceLocator = this._container.GetExportedValue<IServiceLocator>();
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        protected virtual void ConfigureContainer()
        {
            this.RegisterBootstrapperProvidedTypes();
        }

        protected void RegisterBootstrapperProvidedTypes()
        {
            this._container.ComposeExportedValue<ILoggerFacade>(this.Logger);
            //this.Container.ComposeExportedValue<IModuleCatalog>(this.ModuleCatalog);
            this._container.ComposeExportedValue<IServiceLocator>(new MefServiceLocatorAdapter(this._container));
            //this.Container.ComposeExportedValue<AggregateCatalog>(this.AggregateCatalog);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "The default export provider is in the container and disposed by MEF.")]
        private CompositionContainer CreateContainer()
        {
            //CompositionContainer container = new CompositionContainer(this.AggregateCatalog);

            var provider = new ServiceHostExportProvider();
            CompositionContainer container = _factory.CreateCompositionContainer(provider);

            provider.SourceContainer = container;

            return container;
        }

        //protected void ConfigureDirectoryCatalog()
        //{
        //}

        protected void CreateContainerFactory()
        {
            _directoryCatalog = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
            _factory = new DelegateCompositionContainerFactory(ep => new CompositionContainer(this._directoryCatalog, ep));
        }

        //protected void ConfigureAggregateCatalog()
        //{
        // Add this assembly to the catalog.
        //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));

        //IList<Assembly> svrModules = CommonUtility.LookupAssemblies(AppDomain.CurrentDomain.BaseDirectory, typeof(ILighterService));
        //if (svrModules.Count == 0)
        //    throw new DllNotFoundException("Service module not found");

        //foreach(Assembly ass in svrModules)
        //    this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(ass));
        //}

        //protected AggregateCatalog CreateAggregateCatalog()
        //{
        //    return new AggregateCatalog();
        //}

        //public virtual void RegisterDefaultTypesIfMissing()
        //{
        //this.AggregateCatalog = DefaultPrismServiceRegistrar.RegisterRequiredPrismServicesIfMissing(this.AggregateCatalog);
        //}

        protected override DependencyObject CreateShell()
        {
            throw new NotImplementedException();
        }
    }
}
