using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Xaml;

namespace Lighter.Client
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new ShellView();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            //App.Current.MainWindow.Show();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            string catConfigName = AppDomain.CurrentDomain.BaseDirectory + "ModuleConfig.xaml";
            IModuleCatalog modCatalog = XamlServices.Load(catConfigName) as ModuleCatalog;

            return modCatalog;
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // Add this assembly to the catalog.
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));

            // Modules are located in the shell's directory. Both module projects have
            // a post-build step that copies the module assemblies into this directory.
            DirectoryCatalog catalog = new DirectoryCatalog(@".");

            this.AggregateCatalog.Catalogs.Add(catalog);
        }
    }
}
