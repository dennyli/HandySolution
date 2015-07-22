using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;
using Lighter.LoginService.Data.AccountManager;

namespace Lighter.Server.ModuleServiceBase
{
    public interface ILighterModuleService : ILighterService
    {
        IQueryable<Account> Accounts { get; }
    }
}
