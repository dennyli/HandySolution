using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

using Client.ModuleBase.ViewModels;
using Client.Module.UserManager.Interface;
using Client.Module.Common.Interface;
using Lighter.UserManagerService.Model;
using Client.Module.UserManager.Models;

namespace Client.Module.UserManager.ViewModels
{
    /// <summary>
    /// UserManagerViewModel for UserManagerView.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class UserManagerViewModel : ViewModelBase, INavigationAware, IUserManagerViewModel
    {
        [ImportingConstructor]
        public UserManagerViewModel(IUserManagerDataService dataService, IEventAggregator eventAggregator, IRegionManager regionManager)
            : base(dataService, eventAggregator, regionManager)
        {
            AccountsModel = (_dataService as IUserManagerDataService).GetAccounts();
        }

        #region INavigationAware Members
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        #endregion

        #region Fields
        public Accounts AccountsModel { get; private set; }

        //public ObservableCollection<AccountDTO> Accounts;
        //public ObservableCollection<DepartmentDTO> Departments;
        //public ObservableCollection<RoleDTO> Roles;
        #endregion

    }
}
