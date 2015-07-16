namespace Lighter.ServiceManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;
    using System.Windows;
    using Composition;
    using Hosting;
    using Lighter.BaseService.Interface;
    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Logging;
    using Microsoft.Practices.Prism.MefExtensions;
    using Microsoft.Practices.ServiceLocation;
    using Utility;

    /// <summary>
    /// Defines a service manager.
    /// </summary>
    [Export(typeof(IServiceHostManager))]
    public class ServiceHostManager : Bootstrapper, IServiceHostManager
    {
        #region Fields
        private CompositionContainer _container;
        private ICompositionContainerFactory _factory;
        //private AggregateCatalog _aggregateCatalog;
        private DirectoryCatalog _directoryCatalog;
        #endregion

        #region Constructor
        public ServiceHostManager()
        {
            //this._aggregateCatalog = CreateAggregateCatalog();
            //_factory = new DelegateCompositionContainerFactory(ep => new CompositionContainer(this._aggregateCatalog, ep));

            _directoryCatalog = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
            _factory = new DelegateCompositionContainerFactory(ep => new CompositionContainer(this._directoryCatalog, ep));
        }
 

        /// <summary>
        /// Initialises a new instance of <see cref="ServiceHostManager"/>.
        /// </summary>
        /// <param name="factory">The container factory.</param>
        public ServiceHostManager(ICompositionContainerFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            _factory = factory;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="ServiceHostManager"/>.
        /// </summary>
        /// <param name="factory">The delegate used to create a container.</param>
        public ServiceHostManager(Func<ExportProvider[], CompositionContainer> factory)
            : this(new DelegateCompositionContainerFactory(factory)) { }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the set of service hosts.
        /// </summary>
        public IEnumerable<ExportServiceHost> Services { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initialises the service manager.
        /// </summary>
        private void LookupServices()
        {
            Services = _container.GetExportedValues<ExportServiceHost>();
        }
        #endregion

        #region Bootstrapper
        //protected override void ConfigureServiceLocator()
        //{
        //    throw new NotImplementedException();
        //}

        //protected override DependencyObject CreateShell()
        //{
        //    throw new NotImplementedException();
        //}

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
            //this._container.ComposeExportedValue<AggregateCatalog>(this._aggregateCatalog);
            this._container.ComposeExportedValue(this);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "The default export provider is in the container and disposed by MEF.")]
        private CompositionContainer CreateContainer()
        {
            //CompositionContainer container = new CompositionContainer(this._aggregateCatalog);
            //return container;
            var provider = new ServiceHostExportProvider();
            CompositionContainer _container = _factory.CreateCompositionContainer(provider);
            //_container.ComposeExportedValue(this);

            provider.SourceContainer = _container;

            return _container;
        }

        protected void ConfigureAggregateCatalog()
        {
            // Add this assembly to the catalog.
            //this._aggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ServiceHostManager).Assembly));

            //IList<Assembly> svrModules = CommonUtility.LookupAssemblies(AppDomain.CurrentDomain.BaseDirectory, typeof(ILighterService));
            //if (svrModules.Count == 0)
            //    throw new DllNotFoundException("Service module not found");

            //foreach (Assembly ass in svrModules)
            //    this._aggregateCatalog.Catalogs.Add(new AssemblyCatalog(ass));
        }

        protected AggregateCatalog CreateAggregateCatalog()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ServiceHostManager).Assembly));
            return catalog;
        }

        public virtual void RegisterDefaultTypesIfMissing()
        {
            //this._aggregateCatalog = DefaultPrismServiceRegistrar.RegisterRequiredPrismServicesIfMissing(this._aggregateCatalog);
        }

        protected override DependencyObject CreateShell()
        {
            throw new NotImplementedException();
        }

        public override void Run(bool runWithDefaultConfiguration = true)
        {
            this.Logger = this.CreateLogger();
            if (this.Logger == null)
                throw new InvalidOperationException("No Logger Facade Found!");

            this.Logger.Log("Logger created in service", Category.Debug, Priority.Low);

            //this.Logger.Log("Creating Aggregate Catalog For MEF Service", Category.Debug, Priority.Low);
            //if (this._aggregateCatalog == null)
            //    this._aggregateCatalog = this.CreateAggregateCatalog();

            //this.Logger.Log("Configuring Aggregate Catalog For MEF Service", Category.Debug, Priority.Low);
            //this.ConfigureAggregateCatalog();

            //this.RegisterDefaultTypesIfMissing();

            this.Logger.Log("Creating Mef Container", Category.Debug, Priority.Low);
            this._container = this.CreateContainer();

            this.Logger.Log("Configuring Mef Container", Category.Debug, Priority.Low);
            this.ConfigureContainer();

            this.Logger.Log("Configuring Service Locator Singleton for Service", Category.Debug, Priority.Low);
            this.ConfigureServiceLocator();

            this.Logger.Log("Registering Framework Exception Types", Category.Debug, Priority.Low);
            this.RegisterFrameworkExceptionTypes();

            this.Logger.Log(" LighterServiceHostBootstrapper Sequence Completed", Category.Debug, Priority.Low);
            LookupServices();
        }
        #endregion // Bootstrapper
    }
}