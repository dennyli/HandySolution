using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Client.Module.UserManager.ViewModels;
using Client.Module.Common.Tools;
using System.Diagnostics;
using Lighter.UserManagerService.Model;
using Utility;
using Utility.Controls;
using System.Windows;

namespace Client.Module.UserManager.Views
{
    [Export("UserManagerView")]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class UserManagerView : UserControl
    {
        public UserManagerView()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public UserManagerViewModel ViewModel
        {
            get { return this.DataContext as UserManagerViewModel; }
            set { this.DataContext = value; }
        }

        private void gridAccounts_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;

            //DataGridCell cell = e.Row.GetCell(0);
            //cell.Content = e.Row.GetIndex() + 1;
        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AccountDTO dto = gridAccounts.SelectedItem as AccountDTO;
            MessageBox.Show(dto.ToString());
        }

    }
}
