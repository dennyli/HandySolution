using System;
using System.Collections.Generic;
using System.ServiceModel;
using Lighter.Server.ModuleServiceBase;
using Lighter.Server.UserManagerService.Interface;
using Lighter.ServiceManager;
using Lighter.ServiceManager.Endpoints;
using Utility;

namespace Lighter.Server.UserManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, Namespace = "http://www.codestar.com/")]
    [ExportService("LighterUserManagerService", typeof(LighterUserManagerService), /*typeof(ILighterLoginService),*/ 1), TcpEndpoint(40002)]
    public class LighterUserManagerService : LighterModuleServiceBase, ILighterUserManagerService
    {
        public override string GetServiceId()
        {
            return "UserManager";
        }

        public override IEnumerable<ModuleDefination> GetModules()
        {
            return new List<ModuleDefination>()
            {
                new ModuleDefination() { Id = "U01", Name="用户管理" },
                new ModuleDefination() { Id = "U02", Name="部门管理" },
                new ModuleDefination() { Id = "U03", Name="岗位管理" },
                new ModuleDefination() { Id = "U04", Name="权限管理" }
            };
        }
    }
}
