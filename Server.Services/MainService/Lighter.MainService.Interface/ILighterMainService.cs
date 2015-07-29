using System;
using System.ServiceModel;
using Lighter.BaseService.Interface;
using Lighter.MainService.Model;
using Lighter.ServiceManager;

namespace Lighter.MainService.Interface
{
    [ServiceContract(CallbackContract = typeof(ILighterConnectCallBack), SessionMode = SessionMode.Required, Namespace = "http://www.codestar.com/")]
    public interface ILighterMainService : ILighterService, IHostedService
    {
        [OperationContract(IsInitiating=false, IsTerminating=false)]
        bool ServiceIsExists(string serviceName);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        Uri[] GetServiceAddress(string serviceName);

        [OperationContract(IsInitiating=true, IsTerminating=false)]
        bool Connect(Client client);

        [OperationContract(IsInitiating = false, IsTerminating = true, IsOneWay = true)]
        void Disconnect(Client client);
    }
}
