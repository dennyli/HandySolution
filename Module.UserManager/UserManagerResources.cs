using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Module.Common.Commands;
using Client.ModuleBase;

namespace Client.Module.UserManager
{
    public class UserManagerResources : ModuleResourcesBase
    {
        internal static string SERVICE_NAME = "LighterUserManagerService";
        internal static string MODULE_NAME = "UserManage";

        public override string GetModuleName()
        {
            return MODULE_NAME;
        }

        /// <summary>
        /// 获取服务端Service的名字，ExportService属性值，必须和服务端Service一致，否者无法提供服务
        /// </summary>
        /// <returns>服务端Service的名字</returns>
        public override string GetServiceName()
        {
            return SERVICE_NAME;
        }

        public override ObservableCollection<CommandInfo> GetCommandInfos(Type owner)
        {
            ObservableCollection<CommandInfo> cmds = new ObservableCollection<CommandInfo>();
            
            // 用户管理
            cmds.Add(new CommandInfo("MU01", CommandDefinitions.CN_USER_MANAGE, 
                CommandDefinitions.CT_USER_MANAGE, 
                CommandDefinitions.CI_USER_MANAGE,
                new KeyGesture(Key.U, ModifierKeys.Control | ModifierKeys.Shift), 
                owner, 
                CommandDefinitions.CV_USER_MANAGE,
                CommandAnchor.MenuItem | CommandAnchor.Toolbar));

            // 部门管理
            cmds.Add(new CommandInfo("MU02", CommandDefinitions.CN_DEPART_MANAGE,
                CommandDefinitions.CT_DEPART_MANAGE,
                CommandDefinitions.CI_DEPART_MANAGE,
                new KeyGesture(Key.D, ModifierKeys.Control | ModifierKeys.Shift),
                owner,
                CommandDefinitions.CV_DEPART_MANAGE,
                CommandAnchor.MenuItem));

            // 角色管理
            cmds.Add(new CommandInfo("MU04", CommandDefinitions.CN_ROLE_MANAGE,
                CommandDefinitions.CT_ROLE_MANAGE,
                CommandDefinitions.CI_ROLE_MANAGE,
                new KeyGesture(Key.R, ModifierKeys.Control | ModifierKeys.Shift),
                owner,
                CommandDefinitions.CV_ROLE_MANAGE,
                CommandAnchor.MenuItem));

            return cmds ;
        }
    }
}
