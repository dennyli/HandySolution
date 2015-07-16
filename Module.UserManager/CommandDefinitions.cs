﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Module.UserManager
{
    public class CommandDefinitions
    {
        public const string CU_TOPMENU_TEXT = "系统设置";

        // 用户管理
        public const string CN_USER_MANAGE = "UserManageCommand";
        public const string CT_USER_MANAGE = "用户管理";
        public const string CI_USER_MANAGE = "/Module.UserManager;component/Images/user.png";
        public const string CV_USER_MANAGE = "UserManagerView";

        // 部门管理
        public const string CN_DEPART_MANAGE = "DepartmentManageCommand";
        public const string CT_DEPART_MANAGE = "部门管理";
        public const string CI_DEPART_MANAGE = "/Module.UserManager;component/Images/users.png";
        public const string CV_DEPART_MANAGE = "DepartmentManagerView";
    }
}
