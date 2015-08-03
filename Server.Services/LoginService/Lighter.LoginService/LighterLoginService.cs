using System;
using System.ComponentModel.Composition;
using System.ServiceModel;
using Lighter.LoginService.Interface;
using Lighter.LoginService.LoginData;
using Lighter.LoginService.Model;
using Lighter.ServiceManager;
using Lighter.ServiceManager.Endpoints;
using Lighter.ServiceManager.TokenValidation;
using Utility;

namespace Lighter.LoginService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, Namespace = "http://www.codestar.com/")]
    [ExportService("LighterLoginService", "账户登录服务", typeof(LighterLoginService), typeof(ILighterLoginService), 1, TokenValidationMode.Uncheck), TcpEndpoint(40002)]
    //[ServiceErrorHandlerBehavior(typeof(ServiceErrorHandler))]
    public class LighterLoginService : ILighterLoginService, IDisposable
    {
        [Import]
        protected ILighterLoginDataService _dataService;

        public OperationResult Login(LoginInfo info)
        {
            return _dataService.Login(info);
        }

        public OperationResult Logout(string userId)
        {
            return _dataService.Logout(userId);
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

        public void Initialize()
        {
            
        }

        #region IDisposable 成员

        public void Dispose()
        {
            
        }

        #endregion
    }
}
