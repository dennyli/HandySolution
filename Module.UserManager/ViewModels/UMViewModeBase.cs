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
using Lighter.UserManagerService.Model;
using System.ComponentModel;

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
            foreach(AccountDTO acc in accounts)
                acc.PropertyChanged += new PropertyChangedEventHandler(acc_PropertyChanged);

            return accounts;
        }

        void acc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DepartmentName")
            {
                AccountDTO acc = sender as AccountDTO;
                acc.DepartmentId = GetDepartments().Single<DepartmentDTO>(dto => dto.Name == acc.DepartmentName).Id;
            }
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
