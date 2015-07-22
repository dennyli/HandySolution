using System.Collections.Generic;
using System.Linq;
using Lighter.BaseService.Interface;
using Lighter.Data.Account;
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
