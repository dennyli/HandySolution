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
    public class RoleManagerViewModel : UMViewModeBase, IRoleManagerViewModel
    {
        [ImportingConstructor]
        public RoleManagerViewModel(IRoleManagerDataService dataService, IEventAggregator eventAggregator, IRegionManager regionManager)
            : base(dataService, eventAggregator, regionManager)
        {
        }

        #region Fields
        public string Title
        {
            get { return CommandDefinitions.CT_ROLE_MANAGE; }
        }
        #endregion
    }
}
