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
using Client.Module.UserManager.ViewModels;

namespace Client.Module.UserManager.Views
{
    /// <summary>
    /// RoleManagerView.xaml 的交互逻辑
    /// </summary>
    [Export("RoleManagerView")]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class RoleManagerView : UserControl
    {
        public RoleManagerView()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public RoleManagerViewModel ViewModel
        {
            get { return this.DataContext as RoleManagerViewModel; }
            set { this.DataContext = value; }
        }
    }
}
