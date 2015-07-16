using System.ComponentModel.Composition;
using Client.Module.UserManager.Interface;
using Client.Module.UserManager.Models;
using Client.ModuleBase.ViewModels;
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
            DepartmentsModel = _dataService.GetModel() as Departments;
        }


        public Departments DepartmentsModel { get; set; }

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
    }
}
