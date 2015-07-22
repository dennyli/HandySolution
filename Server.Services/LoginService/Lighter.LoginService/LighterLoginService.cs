using System;
using System.Linq;
using System.ServiceModel;
using Lighter.LoginService.Data;
using Lighter.LoginService.Interface;
using Lighter.LoginService.Model;
using Lighter.ServiceManager;
using Lighter.ServiceManager.Endpoints;
using Utility;
using Lighter.LoginService.Data.AccountManager;
using System.Text;

namespace Lighter.LoginService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, Namespace = "http://www.codestar.com/")]
    [ExportService("LighterLoginService", typeof(LighterLoginService), /*typeof(ILighterLoginService),*/ 1), TcpEndpoint(40002)]
    public class LighterLoginService : LighterLoginDataService, ILighterLoginService
    {
        public OperationResult Login(LoginInfo info)
        {
            return base.Login(info.Account, info.Password);
        }

        public OperationResult Logout(LoginInfo info)
        {
            return base.Logout(info.Account);
        }

        //public string GetAllAccounts()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (Account a in Accounts)
        //    {
        //        sb.AppendLine(a.ToString());
        //    }

        //    return sb.ToString();
        //}

        public override string GetServiceId()
        {
            return "Login";
        }
    }
}
