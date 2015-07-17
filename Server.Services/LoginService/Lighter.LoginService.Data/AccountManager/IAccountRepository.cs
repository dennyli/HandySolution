using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Server.Infrastructure;

namespace Lighter.LoginService.Data.AccountManager
{
    public interface IAccountRepository : IRepository<Account, Int32>
    {
    }
}
