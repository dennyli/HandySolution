using System.ServiceModel;
using Lighter.BaseService.Interface;
using Lighter.LoginService.Model;
using Lighter.ServiceManager;
using Utility;

namespace Lighter.LoginService.Interface
{
    [ServiceContract(SessionMode=SessionMode.Required, Namespace = "http://www.codestar.com/")]
    public interface ILighterLoginService : ILighterService, IHostedService
    {
        [OperationContract(IsInitiating = true, IsTerminating = false)]
        OperationResult Login(LoginInfo info);

        [OperationContract(IsInitiating = false, IsTerminating = true)]
        OperationResult Logout(LoginInfo info);

        //[OperationContract(IsInitiating = false, IsTerminating = false)]
        //string GetAllAccounts();
    }
}
