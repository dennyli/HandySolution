using System.ServiceModel;
using Lighter.ServiceManager;
using Lighter.MainService.Model;
using Lighter.BaseService.Interface;
using System;

namespace Lighter.MainService.Interface
{
    [ServiceContract(CallbackContract=typeof(ILighterConnectCallBack), SessionMode=SessionMode.Required)]
    public interface ILighterMainService : ILighterService, IHostedService
    {
        [OperationContract]
        bool ServiceIsExists(string serviceName);

        [OperationContract]
        Uri[] GetServiceAddress(string serviceName);

        [OperationContract]
        bool Connect(Client client);

        [OperationContract]
        void Disconnect(Client client);
    }
}
