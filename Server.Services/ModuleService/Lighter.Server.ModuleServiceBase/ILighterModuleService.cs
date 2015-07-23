using System.Collections.Generic;
using System.Linq;
using Lighter.BaseService;
using Lighter.Data;

namespace Lighter.ModuleServiceBase
{
    public interface ILighterModuleService : ILighterService
    {
        IQueryable<Account> Accounts { get; }

        string GetServiceId();
        IEnumerable<Module> GetModules();
    }
}
