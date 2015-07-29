using System.Collections.Generic;
using System.Linq;
using Lighter.BaseService;
using System.ServiceModel;
using Lighter.ModuleServiceBase.Model;
using Lighter.ServiceManager;

namespace Lighter.ModuleServiceBase.Interface
{
    [ServiceContract(SessionMode = SessionMode.Required, Namespace = "http://www.codestar.com/")]
    public interface ILighterModuleService : ILighterSessionService, IHostedService
    {
        [OperationContract]
        string GetServiceId();
        [OperationContract]
        List<ModuleDTO> GetModules();
    }
}
