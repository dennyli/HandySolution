using System.ServiceModel;
using Lighter.BaseService.Interface;
using Lighter.LoginService.Model;
using Lighter.ServiceManager;
using Utility;

namespace Lighter.LoginService.Interface
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ILighterLoginCallback), Namespace = "http://www.codestar.com/")]
    public interface ILighterLoginService : ILighterService, IHostedService
    {
        [OperationContract(IsInitiating = true, IsTerminating = false, IsOneWay = true)]
        void Login(LoginInfo info);

        [OperationContract(IsInitiating = false, IsTerminating = true, IsOneWay = true)]
        void Logout(string userId);

        //[OperationContract(IsInitiating = false, IsTerminating = false)]
        //string GetAllAccounts();
    }
}
