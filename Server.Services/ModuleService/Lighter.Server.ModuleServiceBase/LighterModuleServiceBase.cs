using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService;
using System.ComponentModel.Composition;
using Lighter.LoginService.Data.AccountManager;

namespace Lighter.Server.ModuleServiceBase
{
    public class LighterModuleServiceBase : LighterServiceBase, ILighterModuleService
    {
        [Import]
        protected IAccountRepository AccountRepository { get; set; }

        public IQueryable<Account> Accounts
        {
            get { return AccountRepository.Entities; }
        }
    }
}
