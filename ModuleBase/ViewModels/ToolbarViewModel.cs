﻿using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Client.Module.Common.Commands;
using Client.Module.Common.Events;
using Client.Module.Common.Tools;
using Client.ModuleBase.Views;
using Lighter.Client.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Lighter.Client.Infrastructure.Interface;

namespace Client.ModuleBase.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ToolbarViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IServiceLocator _serviceLocator;

        private readonly ILighterClientContext _lighterContext;

        [ImportingConstructor]
        public ToolbarViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IServiceLocator serviceLocator, ILighterClientContext lighterContext)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;

            _lighterContext = lighterContext;

            ToolbarItems = new ObservableCollection<ToolbarCommand>();

            _eventAggregator.GetEvent<RoutedUICommandSelectedEvent>().Subscribe(RoutedUICommandSelected);
            _eventAggregator.GetEvent<ToolbarButtonAddedEvent>().Subscribe(ToolbarButtonAdded);
        }

        private void ToolbarButtonAdded(ObservableCollection<CommandInfo> commandInfos)
        {
            if ((commandInfos != null) && (commandInfos.Count > 0))
            {
                ToolbarView view = _serviceLocator.GetInstance<ToolbarView>();
                foreach (CommandInfo info in commandInfos)
                {
                    if (!info.Anchor.HasFlag(CommandAnchor.Toolbar))
                        continue;

                    if (info.CommandBinding != null)
                        view.CommandBindings.Add(info.CommandBinding);

                    ToolbarItems.Add(new ToolbarCommand(info.Command, info.Icon, info.Text));
                }

                this.RaisePropertyChanged("ToolbarItems");
            }
        }

        private ICommand _lastRoutedUICommand = null;
        private void RoutedUICommandSelected(ICommand command)
        {
            if (command == null)
                return;
            if (_lastRoutedUICommand == command)
                return;

            _lastRoutedUICommand = command;
            RoutedUICommand routedUICommand = command as RoutedUICommand;

            CommandInfo info = _lighterContext.FindCommandInfoByName(routedUICommand.Name);
            if (info == null)
                return;

            // Open New View
            _regionManager.RequestNavigate(RegionNames.MainRegion, QueryStringBuilder.Construct(info.ViewKey, null));

            // Reset All Toolbar Button Background
            foreach (ToolbarCommand cmd in ToolbarItems)
            {
                if (cmd.Command == command)
                    cmd.SetBackBrushToSelected();
                else
                    cmd.ResetBackBrushToDefault();
            }
        }

        /// <summary>
        /// Property for UI Binding
        /// </summary>
        public ObservableCollection<ToolbarCommand> ToolbarItems { get; private set; }
    }
}
