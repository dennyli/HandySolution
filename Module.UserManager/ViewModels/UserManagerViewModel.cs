using System.ComponentModel.Composition;
using Client.Module.UserManager.Interface.Services;
using Client.Module.UserManager.Interface.ViewModels;
using Client.Module.UserManager.Models;
using Client.ModuleBase.ViewModels;
using Lighter.UserManagerService.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace Client.Module.UserManager.ViewModels
{
    /// <summary>
    /// UserManagerViewModel for UserManagerView.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class UserManagerViewModel : UMViewModeBase, IUserManagerViewModel
    {
        [ImportingConstructor]
        public UserManagerViewModel(IUserManagerDataService dataService, IEventAggregator eventAggregator, IRegionManager regionManager)
            : base(dataService, eventAggregator, regionManager)
        {
        }
        #region Fields

        public Accounts Accounts { get { return this.GetAccounts(); } }

        public string Title
        {
            get { return CommandDefinitions.CT_USER_MANAGE; }
        }

        #endregion

    }
}
