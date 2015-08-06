using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;
using System.Xaml;
using Lighter.Client.View;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;

namespace Lighter.Client
{
    public class LighterBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            //return new ShellView();

            return this.Container.GetExportedValue<ShellView>();
        }

        protected override void InitializeShell()
        {

            App.Current.MainWindow = (Window)this.Shell;
            //App.Current.MainWindow.Show();

            base.InitializeShell();
        }

        protected override void InitializeModules()
        {
            IEnumerable<Lazy<object, object>> exports = this.Container.GetExports(typeof(IModuleManager), null, null);
            if ((exports != null) && (exports.Count() > 0))
                base.InitializeModules();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //string catConfigName = AppDomain.CurrentDomain.BaseDirectory + "ModuleConfig.xaml";
            //IModuleCatalog modCatalog = XamlServices.Load(catConfigName) as ModuleCatalog;

            DirectoryModuleCatalog catalog = new DirectoryModuleCatalog();
            catalog.ModulePath = AppDomain.CurrentDomain.BaseDirectory;
            catalog.Initialize();

            return catalog;
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // Add this assembly to the catalog.
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(LighterBootstrapper).Assembly));

            // Modules are located in the shell's directory. Both module projects have
            // a post-build step that copies the module assemblies into this directory.
            DirectoryCatalog catalogDLL = new DirectoryCatalog(@".\", "*.dll");
            DirectoryCatalog catalogExe = new DirectoryCatalog(@".\", "*.exe");

            this.AggregateCatalog.Catalogs.Add(catalogDLL);
            this.AggregateCatalog.Catalogs.Add(catalogExe);
        }

        //public void ComposeExternelParts(object[] parts)
        //{
        //    if (!_bInitialized)
        //        Initialize();

        //    this.Container.ComposeParts(parts);
        //}

        public T GetExportedValue<T>()
        {
            Initialize();

            T t = this.Container.GetExportedValue<T>();
            this.Container.ComposeParts(t);

            return t;
        }

        private bool _bInitialized = false;
        public void Initialize()
        {
            if (!_bInitialized)
            {
                Run();

                _bInitialized = true;
            }
        }

        public void RunShell()
        {
            if (!_bInitialized)
                Initialize();

            this.InitializeModules();

            App.Current.MainWindow.Show();
        }
    }
}
