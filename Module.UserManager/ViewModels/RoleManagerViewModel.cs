using System.ComponentModel.Composition;
using Client.Module.UserManager.Interface.ViewModels;
using Client.ModuleBase.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Client.Module.UserManager.Interface.Services;
using Microsoft.Practices.Prism.Events;
using Client.Module.UserManager.Models;
using Lighter.UserManagerService.Interface;

namespace Client.Module.UserManager.ViewModels
{
    /// <summary>
    /// RoleManagerViewModel for RoleManagerView.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RoleManagerViewModel : ViewModelBase, INavigationAware, IRoleManagerViewModel
    {
        [ImportingConstructor]
        public RoleManagerViewModel(IRoleManagerDataService dataService, IEventAggregator eventAggregator, IRegionManager regionManager)
            : base(dataService, eventAggregator, regionManager)
        {
            RolesModel = (_dataService as IRoleManagerDataService).GetRoles();
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
            if (null == _dataService.GetServerService(UserManagerResources.SERVICE_NAME))
                _dataService.InitilizeServerService<ILighterUserManagerService>(UserManagerResources.SERVICE_NAME, null);
        }
        #endregion

        #region Fields
        public Roles RolesModel { get; private set; }

        public string Title
        {
            get { return CommandDefinitions.CT_ROLE_MANAGE; }
        }
        #endregion
    }
}
