using System.ServiceModel;
using Lighter.LoginService.Data;
using Lighter.LoginService.Model;
using Lighter.ServiceManager;
using Utility;

namespace Lighter.LoginService.Interface
{
    [ServiceContract]
    public interface ILighterLoginService : IHostedService
    {
        [OperationContract]
        OperationResult Login(LoginInfo info);

        [OperationContract]
        OperationResult Logout(LoginInfo info);

        [OperationContract]
        string GetAllAccounts();
    }
}
