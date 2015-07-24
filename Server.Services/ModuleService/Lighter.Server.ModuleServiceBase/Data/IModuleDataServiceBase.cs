using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Data;

namespace Lighter.ModuleServiceBase.Data
{
    public interface IModuleDataServiceBase
    {
        IQueryable<Account> Accounts { get; }
    }
}
