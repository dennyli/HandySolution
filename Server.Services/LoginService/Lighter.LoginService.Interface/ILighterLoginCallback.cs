using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ServiceModel;

namespace Lighter.LoginService.Interface
{
    public interface ILighterLoginCallback
    {
        [OperationContract(IsOneWay = true)]
        void LoginResult(OperationResult or);

        [OperationContract(IsOneWay = true)]
        void LogoutResult(OperationResult or);
    }
}
