using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Lighter.Data.Repositories;
using Lighter.Data;

namespace Lighter.ModuleServiceBase.Data
{
    public class ModuleDataServiceBase : IModuleDataServiceBase
    {
        [Import]
        protected IAccountRepository AccountRepository { get; set; }

        public IQueryable<Account> Accounts
        {
            get { return AccountRepository.Entities; }
        }
    }
}
