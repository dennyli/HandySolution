using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

using Client.ModuleBase.ViewModels;
using Client.Module.UserManager.Interface;
using Client.Module.Common.Interface;

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

    }
}
