
using Client.Module.Common.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;

namespace Client.ModuleBase.ViewModels
{
    /// <summary>
    /// ViewModelBase
    /// </summary>
    //[Export]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public abstract class ViewModelBase : NotificationObject, INavigationAware, IViewModel
    {
        protected readonly IDataService _dataService;
        protected readonly IEventAggregator _eventAggregator;
        protected readonly IRegionManager _regionManager;

        //[ImportingConstructor]
        public ViewModelBase(IDataService dataService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
        }

        #region INavigationAware Members
        public abstract bool IsNavigationTarget(NavigationContext navigationContext);

        public abstract void OnNavigatedFrom(NavigationContext navigationContext);

        public abstract void OnNavigatedTo(NavigationContext navigationContext);
        #endregion
    }
}
