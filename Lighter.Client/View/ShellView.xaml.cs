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

namespace Lighter.Client
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

        [Import(AllowRecomposition = false)]
        public ShellViewModel ViewModel
        {
            get { return this.DataContext as ShellViewModel; }
            set { this.DataContext = value; }
        }

        protected override void OnClosed(EventArgs e)
        {
            ViewModel.ExitCommand.Execute(null);

            //base.OnClosed(e);
        }
    }
}
