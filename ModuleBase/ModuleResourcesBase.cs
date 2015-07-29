using System;
using Client.Module.Common.Commands;
using Client.Module.Common.Interface;
using System.Collections.ObjectModel;

namespace Client.ModuleBase
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


        public virtual string GetServiceName()
        {
            return null;
        }
    }
}
