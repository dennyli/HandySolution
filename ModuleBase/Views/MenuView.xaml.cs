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
using Module.Common;
using System.ComponentModel.Composition;
using ModuleBase.ViewModels;

namespace ModuleBase.Views
{
    /// <summary>
    /// MenuView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public MenuViewModel ViewModel
        {
            get { return this.DataContext as MenuViewModel; }
            set { this.DataContext = value; }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("mmm");
        }
    }
}
