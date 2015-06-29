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
using Module.UserManager.ViewModels;
using System.ComponentModel.Composition;

namespace Module.UserManager.Views
{
    /// <summary>
    /// DepartmentManagerView.xaml 的交互逻辑
    /// </summary>
    [Export("DepartmentManagerView")]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class DepartmentManagerView : UserControl
    {
        public DepartmentManagerView()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = true)]
        public DepartmentManagerViewModel ViewModel
        {
            get { return this.DataContext as DepartmentManagerViewModel; }
            set { this.DataContext = value; }
        }

        private void dgDepartment_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;    //设置行表头的内容值
        }
    }
}
