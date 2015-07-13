using System.ServiceModel;

namespace Lighter.Service.Interface
{
    [ServiceContract(CallbackContract=typeof(ILoginCallBack), SessionMode=SessionMode.Required)]
    public interface ILighterService
    {
        
    }

    
}
