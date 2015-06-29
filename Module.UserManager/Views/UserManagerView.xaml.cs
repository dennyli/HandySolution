using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

using Module.UserManager.ViewModels;

namespace Module.UserManager.Views
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
    }
}
