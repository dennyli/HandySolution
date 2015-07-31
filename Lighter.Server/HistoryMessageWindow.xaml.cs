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
using System.Windows.Shapes;
using Lighter.Server.ViewModel;

namespace Lighter.Server
{
    /// <summary>
    /// HistoryMessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryMessageWindow : Window
    {
        public HistoryMessageWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = new HistoryMessageViewModel(viewModel);

            this.DataContext = _viewModel;
        }

        private HistoryMessageViewModel _viewModel;
    }
}
