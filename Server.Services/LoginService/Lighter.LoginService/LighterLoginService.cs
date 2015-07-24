using System.ServiceModel;
using Lighter.LoginService.Interface;
using Lighter.LoginService.LoginData;
using Lighter.LoginService.Model;
using Lighter.ServiceManager;
using Lighter.ServiceManager.Endpoints;
using Utility;
using System.ComponentModel.Composition;

namespace Lighter.LoginService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, Namespace = "http://www.codestar.com/")]
    [ExportService("LighterLoginService", typeof(LighterLoginService), /*typeof(ILighterLoginService),*/ 1), TcpEndpoint(40002)]
    public class LighterLoginService : ILighterLoginService
    {
        [Import]
        protected ILighterLoginDataService _dataService;

        public OperationResult Login(LoginInfo info)
        {
            return _dataService.Login(info.Account, info.Password);
        }

        public OperationResult Logout(LoginInfo info)
        {
            return _dataService.Logout(info.Account);
        }

        //public string GetAllAccounts()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (Account a in _dataService.Accounts)
        //    {
        //        sb.AppendLine(a.ToString());
        //    }

        //    return sb.ToString();
        //}
    }
}
