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

namespace Client.ModuleBase
{
    [ModuleExport("Client.ModuleBase.ModuleBaseInit", typeof(IModule))]
    public class ModuleBaseInit : IModule, IModuleInit
    {
        protected readonly IRegionManager _regionManager;
        protected readonly IServiceLocator _serviceLocator;
        protected readonly IEventAggregator _eventAggregator;

        protected IModuleResources _resources;

        [ImportingConstructor]
        public ModuleBaseInit(IRegionManager regionManager, IEventAggregator eventAggregator, IServiceLocator serviceLocator)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;

            _resources = null;
        }

        #region IModule Members

        public virtual void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MenuRegion, () => _serviceLocator.GetInstance<MenuView>());
            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, () => _serviceLocator.GetInstance<ToolbarView>());
        }

        #endregion

        protected void MenuItemAndToolbarCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        protected void MenuItemAndToolbarCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _eventAggregator.GetEvent<RoutedUICommandSelectedEvent>().Publish(e.Command);
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
            throw new NotImplementedException("CreateModuleResources must be implemented in subclass of ModuleBaseInit");
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
            LighterContext.GetInstance().AddComandInfos(commandInfos);
        }
    }
}
