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
        private LighterBootstrapper _bootstrapper = null;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (_bootstrapper == null)
                _bootstrapper = new LighterBootstrapper();

#if WITH_TOKEN
            LoginView loginView = _bootstrapper.GetExportedValue<LoginView>();
            loginView.InitializeEventAggregator();

            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            if (loginView.ShowDialog() == true)
            {
                this.ShutdownMode = ShutdownMode.OnMainWindowClose;
#endif
            _bootstrapper.RunShell();
#if WITH_TOKEN
            }
            else
                Shutdown();
#endif
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_bootstrapper != null)
            {
                _bootstrapper.Dispose();
                _bootstrapper = null;
            }

            base.OnExit(e);
        }
    }
}
