using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;
using Client.ModuleBase.ViewModels;
using Client.Module.Common.Interface;
using Microsoft.Practices.Prism.Events;
using Lighter.UserManagerService.Interface;
using Client.Module.UserManager.Models;
using Client.Module.UserManager.Interface.Services;

namespace Client.Module.UserManager.ViewModels
{
    public class UMViewModeBase : ViewModelBase, INavigationAware
    {
        public UMViewModeBase(IDataService dataService, IEventAggregator eventAggregator, IRegionManager regionManager)
            : base(dataService, eventAggregator, regionManager)
        {
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
            InitilizeServerService<ILighterUserManagerService>(UserManagerResources.SERVICE_NAME, null);
        }
        #endregion

        #region Methods
        protected Accounts GetAccounts()
        {
            Accounts accounts = (_dataService as IUMDataService).GetAccounts();
            return accounts;
        }

        protected Roles GetRoles()
        {
            return (_dataService as IUMDataService).GetRoles();
        }

        protected Departments GetDepartments()
        {
            return (_dataService as IUMDataService).GetDepartments();
        }
        #endregion
    }
}
