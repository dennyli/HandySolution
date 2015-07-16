using System.ComponentModel.Composition;
using System.Windows.Controls;
using Client.ModuleBase.ViewModels;

namespace Client.ModuleBase.Views
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
