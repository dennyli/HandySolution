using System.Collections.Generic;
using System.Linq;
using Lighter.BaseService.Interface;
using Lighter.Data;
using Utility;

namespace Lighter.Server.ModuleServiceBase
{
    public interface ILighterModuleService : ILighterService
    {
        IQueryable<Account> Accounts { get; }

        string GetServiceId();
        IEnumerable<ModuleDefination> GetModules();
    }
}
