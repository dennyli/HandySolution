using System.Collections.Generic;
using System.Linq;
using Lighter.BaseService;
using Lighter.Data;
using System.ServiceModel;
using Lighter.ModuleServiceBase.Data;

namespace Lighter.ModuleServiceBase
{
    public interface ILighterModuleService : ILighterService
    {
        [OperationContract]
        string GetServiceId();
        [OperationContract]
        IEnumerable<ModuleDTO> GetModules();
    }
}
