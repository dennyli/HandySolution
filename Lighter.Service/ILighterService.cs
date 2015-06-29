using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Lighter.Service
{
    [ServiceContract(CallbackContract=typeof(ILoginCallBack), SessionMode=SessionMode.Required)]
    public interface ILighterService
    {
        
    }

    
}
