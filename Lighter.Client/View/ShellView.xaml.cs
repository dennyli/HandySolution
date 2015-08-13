using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using Lighter.Client.Events;
using Lighter.Client.ViewModel;
using Microsoft.Practices.Prism.Events;
using Lighter.Client.Infrastructure.Events.ServiceEvents;
using Utility.Controls;

namespace Lighter.Client.View
{
    /// <summary>
    /// ShellView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }

        #region Fields
        [Import(AllowRecomposition = false)]
        public ShellViewModel ViewModel
        {
            get { return this.DataContext as ShellViewModel; }
            set { this.DataContext = value; }
        }

        [Import]
        public IEventAggregator EventAggregator { get; set; }

        #endregion

        #region Login result event
        public void InitializeEventAggregator()
        {
            EventAggregator.GetEvent<ServiceEvent>().Subscribe(DoServiceEvent);
        }

        private void DoServiceEvent(ServiceEventArgs args)
        {
            switch (args.Kind)
            {
                case ServiceEventKind.TooBusy:
                case ServiceEventKind.NotFound:
                case ServiceEventKind.CommunicationError:
                    LighterMessageBox.ShowMessageBox(this, args.Message, "提示");
                    break;
                case ServiceEventKind.Closed:
                    break;
            }
        }

        #endregion

        protected override void OnClosed(EventArgs e)
        {
            ViewModel.ExitCommand.Execute(null);

            //base.OnClosed(e);
        }
    }
}
