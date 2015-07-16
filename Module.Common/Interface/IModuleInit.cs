using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Client.Module.Common.Commands;

namespace Client.Module.Common.Interface
{
    public interface IModuleInit
    {
        ObservableCollection<MenuItem> GetMenuItems(ObservableCollection<CommandInfo> commandInfos);
        ObservableCollection<MenuItem> GetMenuItemsWithTopMenuItem(ObservableCollection<CommandInfo> commandInfos, string topMenuHeader, string topMenuName);

        CommandBinding CreateCommandBindingFroUICommand(ICommand command);

        IModuleResources GetModuleResources();

        IModuleResources CreateModuleResources();

        void AddCommandInfosIntoGlobalCommands(ObservableCollection<CommandInfo> commandInfos);
    }
}
