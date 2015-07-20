using System;
using System.ComponentModel.Composition;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Lighter.Server.Infrastructure;

namespace Lighter.LoginService.Data.AccountManager
{
    [Export(typeof(IEntityMapper))]
    public class AccountConfiguration : EntityConfiguration<Account, Int32>
    {
        public AccountConfiguration()
        {
        }
    }
}
