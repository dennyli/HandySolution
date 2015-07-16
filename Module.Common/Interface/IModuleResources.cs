using System;
using System.Collections.ObjectModel;
using Client.Module.Common.Commands;

namespace Client.Module.Common.Interface
{
    public interface IModuleResources
    {
        string GetModuleName();

        ObservableCollection<CommandInfo> GetCommandInfos(Type owner);
    }
}
