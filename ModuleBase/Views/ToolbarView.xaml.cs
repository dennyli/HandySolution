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
using Module.Common.Commands;
using System.ComponentModel.Composition;
using ModuleBase.ViewModels;

namespace ModuleBase.Views
{
    /// <summary>
    /// ToolbarView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ToolbarView : UserControl
    {
        public ToolbarView()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public ToolbarViewModel ViewModel
        {
            get { return this.DataContext as ToolbarViewModel; }
            set { this.DataContext = value; }
        }
    }
}
