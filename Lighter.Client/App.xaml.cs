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
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            LoginWindow login = new LoginWindow();
            if (login.ShowDialog() == true)
            {
                //base.OnStartup(e);

                //var bootstrapper = new Bootstrapper();
                //bootstrapper.Run();
            }
        }
    }
}
