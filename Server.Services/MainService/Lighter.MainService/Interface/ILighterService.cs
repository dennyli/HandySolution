using System.ServiceModel;
using Lighter.ServiceHostManager;

namespace Lighter.MainService.Interface
{
    [ServiceContract(CallbackContract=typeof(ILighterConnectCallBack), SessionMode=SessionMode.Required)]
    public interface ILighterMainService : IHostedService
    {
        
    }

    
}
