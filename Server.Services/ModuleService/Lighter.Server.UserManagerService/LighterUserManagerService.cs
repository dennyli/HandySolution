using System;
using System.Collections.Generic;
using System.ServiceModel;
using Lighter.ModuleServiceBase;
using Lighter.UserManagerService.Interface;
using Lighter.ServiceManager;
using Lighter.ServiceManager.Endpoints;
using Utility;
using Lighter.Data;

namespace Lighter.UserManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, Namespace = "http://www.codestar.com/")]
    [ExportService("LighterUserManagerService", typeof(LighterUserManagerService), /*typeof(ILighterLoginService),*/ 1), TcpEndpoint(40002)]
    public class LighterUserManagerService : LighterModuleServiceBase, ILighterUserManagerService
    {
        public override string GetServiceId()
        {
            return "UserManager";
        }

        public override IEnumerable<Module> GetModules()
        {
            return new List<Module>()
            {
                new Module() { Id = "U01", Name="用户管理", Catalog="账户管理" },
                new Module() { Id = "U02", Name="部门管理", Catalog="账户管理" },
                new Module() { Id = "U03", Name="岗位管理", Catalog="账户管理" },
                new Module() { Id = "U04", Name="权限管理", Catalog="账户管理" },
                new Module() { Id = "U05", Name="模块管理", Catalog="账户管理" }
            };
        }
    }
}
