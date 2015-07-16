using System.ServiceModel;
using Lighter.ServiceManager;
using Lighter.BaseService.Interface;
using System;

namespace Lighter.MainService.Interface
{
    [ServiceContract(CallbackContract=typeof(ILighterConnectCallBack), SessionMode=SessionMode.Required)]
    public interface ILighterMainService : ILighterService, IHostedService
    {
        bool ServiceIsExists(string serviceName);
        Uri GetServiceAddress(string serviceName);
    }
}
