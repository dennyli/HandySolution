using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Client.Module.Common.Commands;
using Lighter.Client.Infrastructure.Implement;

namespace Client.ModuleBase
{
    internal class ModuleResources : ModuleResourcesBase
    {
        /// <summary>
        /// 获取模块名
        /// </summary>
        /// <returns>模块名</returns>
        public override string GetModuleName()
        {
            return "ModuleBase";
        }

        /// <summary>
        /// 获取模块支持的命令列表
        /// </summary>
        /// <param name="owner">命令父类型</param>
        /// <returns>命令列表</returns>
        public override ObservableCollection<CommandInfo> GetCommandInfos(Type owner) { return null; }

        /// <summary>
        /// 获取服务端Service的名字，ExportService属性值，必须和服务端Service一致，否者无法提供服务
        /// 必须在子类中派生提供
        /// </summary>
        /// <returns>服务端Service的名字</returns>
        public override string GetServiceName() { return ServiceFactory.MAIN_SERVICE_NAME; }
    }
}
