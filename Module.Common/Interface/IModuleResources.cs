using System;
using Module.Common.Commands;
using System.Collections.ObjectModel;

namespace Module.Common.Interface
{
    public interface IModuleResources
    {
        string GetModuleName();

        ObservableCollection<CommandInfo> GetCommandInfos(Type owner);
    }
}
