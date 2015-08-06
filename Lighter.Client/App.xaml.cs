using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Lighter.Client.View;

namespace Lighter.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var bootstrapper = new LighterBootstrapper();

            LoginView loginView = bootstrapper.GetExportedValue<LoginView>();
            loginView.InitializeEventAggregator();

            if (loginView.ShowDialog() == true)
            {
                this.ShutdownMode = ShutdownMode.OnMainWindowClose;

                bootstrapper.RunShell();
            }
            else
                Shutdown();
        }
    }
}
