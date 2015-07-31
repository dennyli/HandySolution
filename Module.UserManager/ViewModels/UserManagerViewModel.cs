using System.ComponentModel.Composition;
using Client.Module.UserManager.Interface.Services;
using Client.Module.UserManager.Interface.ViewModels;
using Client.Module.UserManager.Models;
using Client.ModuleBase.ViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

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

        public string Title
        {
            get { return CommandDefinitions.CT_USER_MANAGE; }
        }

        #endregion

    }
}
