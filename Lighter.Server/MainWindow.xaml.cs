using System.Windows;
using Lighter.Server.ViewModel;


namespace Lighter.Server
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainViewModel();

            this.DataContext = _viewModel;

        }

        private MainViewModel _viewModel;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel.ShutdownServices();
        }
    }
}
