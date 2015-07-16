using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;
using Client.Module.Common.Commands;
using Client.Module.Common.Events;
using Client.Module.Common.Interface;
using Client.ModuleBase;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Client.Module.UserManager
{
    [ModuleExport("Client.Module.UserManager.UserManagerModuleInit", typeof(IModule))]
    public class UserManagerModuleInit : ModuleBaseInit
    {
        [ImportingConstructor]
        public UserManagerModuleInit(IRegionManager regionManager, IEventAggregator eventAggregator, IServiceLocator serviceLocator )
            : base(regionManager, eventAggregator, serviceLocator)
        {
        }

        #region IModule Members

        public override void Initialize()
        {
            IModuleResources resources = GetModuleResources();
            ObservableCollection<CommandInfo> commandInfos = resources.GetCommandInfos(typeof(UserManagerModuleInit));
            foreach (CommandInfo info in commandInfos)
            {
                CommandBinding cmdBinding = CreateCommandBindingFroUICommand(info.Command);
                if (cmdBinding != null)
                    info.CommandBinding = cmdBinding;
            }

            AddCommandInfosIntoGlobalCommands(commandInfos);

            _eventAggregator.GetEvent<ToolbarButtonAddedEvent>().Publish(commandInfos);

            ObservableCollection<MenuItem> menuItems = GetMenuItemsWithTopMenuItem(commandInfos, CommandDefinitions.CU_TOPMENU_TEXT, CommandDefinitions.CU_TOPMENU_TEXT);
            _eventAggregator.GetEvent<MenuItemAddedEvent>().Publish(menuItems);
        }

        public override IModuleResources CreateModuleResources()
        {
            return new UserManagerResources();
        }

        #endregion
    }
}
