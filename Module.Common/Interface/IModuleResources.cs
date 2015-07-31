using System;
using System.Collections.ObjectModel;
using Client.Module.Common.Commands;

namespace Client.Module.Common.Interface
{
    public interface IModuleResources
    {
        /// <summary>
        /// 获取模块名
        /// </summary>
        /// <returns>模块名</returns>
        string GetModuleName();

        /// <summary>
        /// 获取模块支持的命令列表
        /// </summary>
        /// <param name="owner">命令父类型</param>
        /// <returns>命令列表</returns>
        ObservableCollection<CommandInfo> GetCommandInfos(Type owner);

        /// <summary>
        /// 获取服务端Service的名字，ExportService属性值，必须和服务端Service一致，否者无法提供服务
        /// 必须在子类中派生提供
        /// </summary>
        /// <returns>服务端Service的名字</returns>
        string GetServiceName();
    }
}
