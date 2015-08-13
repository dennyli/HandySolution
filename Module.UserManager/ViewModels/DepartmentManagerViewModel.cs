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
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DepartmentManagerViewModel : UMViewModeBase, IDepartmentManagerViewModel
    {
        [ImportingConstructor]
        public DepartmentManagerViewModel(IDepartmentManagerDataService dataService, IEventAggregator eventAggregator, IRegionManager regionManager)
            : base(dataService, eventAggregator, regionManager)
        {

        }

        #region Fields
        public string Title
        {
            get { return CommandDefinitions.CT_DEPART_MANAGE; }
        }
        #endregion
    }
}
