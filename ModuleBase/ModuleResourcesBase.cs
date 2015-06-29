using System;
using Module.Common.Commands;
using Module.Common.Interface;
using System.Collections.ObjectModel;

namespace ModuleBase
{
    public abstract class ModuleResourcesBase : IModuleResources
    {
        #region IModuleResources 成员
        public virtual string GetModuleName()
        {
            return "ModuleBase";
        }

        public abstract ObservableCollection<CommandInfo> GetCommandInfos(Type owner);

        #endregion
    }
}
