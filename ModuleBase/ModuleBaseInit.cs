using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;
using Lighter.Client.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Client.Module.Common.Commands;
using Client.Module.Common.Events;
using Client.Module.Common.Interface;
using Client.ModuleBase.Views;
using Lighter.MainService.Interface;
using System.ServiceModel;
using Lighter.BaseService.Interface;
using Lighter.Client.Infrastructure.Interface;
using Lighter.Client.Infrastructure.Implement;

namespace Client.ModuleBase
{
    [ModuleExport("Client.ModuleBase.ModuleBaseInit", typeof(IModule))]
    public class ModuleBaseInit : IModule, IModuleInit
    {
        #region Fields
        protected readonly IRegionManager _regionManager;
        protected readonly IServiceLocator _serviceLocator;
        protected readonly IEventAggregator _eventAggregator;

        protected IModuleResources _resources;

        protected readonly ILighterClientContext _lighterContext;
        #endregion


        [ImportingConstructor]
        public ModuleBaseInit(IRegionManager regionManager, IEventAggregator eventAggregator, IServiceLocator serviceLocator, ILighterClientContext lighterContext)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;
            _lighterContext = lighterContext;

            _resources = null;
        }

        #region IModule Members

        public virtual void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MenuRegion, () => _serviceLocator.GetInstance<MenuView>());
            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, () => _serviceLocator.GetInstance<ToolbarView>());
        }

        #endregion

        #region MenuItem & Toolbar Command
        protected void MenuItemAndToolbarCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        protected void MenuItemAndToolbarCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _eventAggregator.GetEvent<RoutedUICommandSelectedEvent>().Publish(e.Command);
        }
        #endregion MenuItem & Toolbar Command

        #region MenuItem & Toolbar Initialize
        protected virtual void MenuItemAndToolbarInitialize(Type type, string topMenuHeader, string topMenuName)
        {
            IModuleResources resources = GetModuleResources();
            ObservableCollection<CommandInfo> commandInfos = resources.GetCommandInfos(type);
            foreach (CommandInfo info in commandInfos)
            {
                CommandBinding cmdBinding = CreateCommandBindingFroUICommand(info.Command);
                if (cmdBinding != null)
                    info.CommandBinding = cmdBinding;
            }

            AddCommandInfosIntoGlobalCommands(commandInfos);

            _eventAggregator.GetEvent<ToolbarButtonAddedEvent>().Publish(commandInfos);

            ObservableCollection<MenuItem> menuItems = GetMenuItemsWithTopMenuItem(commandInfos, topMenuHeader, topMenuName);
            _eventAggregator.GetEvent<MenuItemAddedEvent>().Publish(menuItems);
        }

        public virtual IModuleResources GetModuleResources()
        {
            if (_resources == null)
                _resources = CreateModuleResources();

            return _resources;
        }

        public virtual ObservableCollection<MenuItem> GetMenuItems(ObservableCollection<CommandInfo> commandInfos)
        {
            ObservableCollection<MenuItem> items = new ObservableCollection<MenuItem>();
            foreach (CommandInfo info in commandInfos)
            {
                if (!info.Anchor.HasFlag(CommandAnchor.MenuItem))
                    continue;

                MenuItem item = new MenuItem();
                item.Command = info.Command;
                if (info.CommandBinding != null)
                    item.CommandBindings.Add(info.CommandBinding);

                items.Add(item);
            }

            return items;
        }

        public virtual ObservableCollection<MenuItem> GetMenuItemsWithTopMenuItem(ObservableCollection<CommandInfo> commandInfos, string topMenuHeader, string topMenuName)
        {
            ObservableCollection<MenuItem> items = new ObservableCollection<MenuItem>();
            // Add top menu
            items.Add(new MenuItem() { Header = topMenuHeader, Name = topMenuName });

            foreach(MenuItem item in GetMenuItems(commandInfos))
                (items[0] as MenuItem).Items.Add(item);

            return items;
        }

        public virtual IModuleResources CreateModuleResources()
        {
            return new ModuleResources();
        }

        public virtual CommandBinding CreateCommandBindingFroUICommand(ICommand command)
        {
            RoutedCommand rcmd = command as RoutedCommand;
            CommandBinding cmdBinding = new CommandBinding();
            cmdBinding.Command = command;
            cmdBinding.CanExecute += new CanExecuteRoutedEventHandler(MenuItemAndToolbarCommand_CanExecute);
            cmdBinding.Executed += new ExecutedRoutedEventHandler(MenuItemAndToolbarCommand_Executed);
            return cmdBinding;
        }

        public virtual void AddCommandInfosIntoGlobalCommands(ObservableCollection<CommandInfo> commandInfos)
        {
            _lighterContext.AddComandInfos(commandInfos);
        }

        #endregion MenuItem & Toolbar Initialize

        #region Server Endpoints
        protected virtual ILighterMainService GetMainService()
        {
            if (_lighterContext != null)
            {
                //IModuleResources resources = GetModuleResources();
                ILighterService service = _lighterContext.FindService(ServiceFactory.MAIN_SERVICE_NAME);
                if (service == null)
                {
                    _lighterContext.CreateMainService();
                    service = _lighterContext.FindService(ServiceFactory.MAIN_SERVICE_NAME);
                }
                
                return service as ILighterMainService;
            }

            return null;
        }

        protected virtual ILighterService CreateService() { return null; }
        #endregion
    }
}
