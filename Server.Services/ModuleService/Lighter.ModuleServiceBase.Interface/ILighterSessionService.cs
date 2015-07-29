using Lighter.BaseService.Interface;
using Utility;

namespace Lighter.ModuleServiceBase.Interface
{
    public interface ILighterSessionService : ILighterService
    {
        OperationResult CheckSession();
    }
}
