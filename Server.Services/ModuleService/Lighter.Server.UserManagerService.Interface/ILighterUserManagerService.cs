using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.ModuleServiceBase;
using System.ServiceModel;

namespace Lighter.UserManagerService.Interface
{
    [ServiceContract(SessionMode = SessionMode.Required, Namespace = "http://www.codestar.com/")]
    public interface ILighterUserManagerService : ILighterModuleService
    {
    }
}
