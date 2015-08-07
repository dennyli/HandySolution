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
    [Export(typeof(DepartmentManagerViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DepartmentManagerViewModel : ViewModelBase, INavigationAware, IDepartmentManagerViewModel
    {
        [ImportingConstructor]
        public DepartmentManagerViewModel(IDepartmentManagerDataService dataService, IEventAggregator eventAggregator, IRegionManager regionManager)
            : base(dataService, eventAggregator, regionManager)
        {
            DepartmentsModel = (_dataService as IDepartmentManagerDataService).GetDepartments();
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
        public Departments DepartmentsModel { get; private set; }
        public string Title
        {
            get { return CommandDefinitions.CT_DEPART_MANAGE; }
        }
        #endregion
    }
}
