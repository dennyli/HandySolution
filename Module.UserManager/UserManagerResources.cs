using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Module.Common.Commands;
using Client.ModuleBase;

namespace Client.Module.UserManager
{
    public class UserManagerResources : ModuleResourcesBase
    {
        public override string GetModuleName()
        {
            return "UserManage";
        }

        public override ObservableCollection<CommandInfo> GetCommandInfos(Type owner)
        {
            ObservableCollection<CommandInfo> cmds = new ObservableCollection<CommandInfo>();
            
            // 用户管理
            cmds.Add(new CommandInfo(CommandDefinitions.CN_USER_MANAGE, 
                CommandDefinitions.CT_USER_MANAGE, 
                CommandDefinitions.CI_USER_MANAGE,
                new KeyGesture(Key.U, ModifierKeys.Control | ModifierKeys.Shift), 
                owner, 
                CommandDefinitions.CV_USER_MANAGE,
                CommandAnchor.MenuItem | CommandAnchor.Toolbar));

            // 部门管理
            cmds.Add(new CommandInfo(CommandDefinitions.CN_DEPART_MANAGE,
                CommandDefinitions.CT_DEPART_MANAGE,
                CommandDefinitions.CI_DEPART_MANAGE,
                new KeyGesture(Key.D, ModifierKeys.Control | ModifierKeys.Shift),
                owner,
                CommandDefinitions.CV_DEPART_MANAGE,
                CommandAnchor.MenuItem));

            return cmds ;
        }
    }
}
