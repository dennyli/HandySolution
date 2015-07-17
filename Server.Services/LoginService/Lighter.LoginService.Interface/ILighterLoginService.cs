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

        [OperationContract(IsOneWay=true)]
        void Logout(LoginInfo info);

        [OperationContract]
        string GetAllAccounts();
    }
}
