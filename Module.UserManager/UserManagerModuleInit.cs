using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;

using ModuleBase;
using ModuleBase.Views;
using Module.UserManager.ViewModels;
using Module.Common.Interface;
using Microsoft.Practices.Prism.Events;
using System.Collections.ObjectModel;
using Module.Common.Commands;
using Module.Common.Events;
using System.Windows.Controls;
using System.Windows.Input;

namespace Module.UserManager
{
    [ModuleExport("Module.UserManager.UserManagerModuleInit", typeof(IModule))]
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
