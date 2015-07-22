using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Server.ModuleServiceBase;
using System.ServiceModel;

namespace Lighter.Server.UserManagerService.Interface
{
    [ServiceContract(SessionMode = SessionMode.Required, Namespace = "http://www.codestar.com/")]
    public interface ILighterUserManagerService : ILighterModuleService
    {
    }
}
