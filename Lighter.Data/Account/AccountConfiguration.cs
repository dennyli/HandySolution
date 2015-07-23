using System;
using System.ComponentModel.Composition;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Lighter.Server.Infrastructure;

namespace Lighter.Data
{
    [Export(typeof(IEntityMapper))]
    public class AccountConfiguration : EntityConfiguration<Account, string>
    {
        public AccountConfiguration()
        {
            HasOptional<Role>(a => a.Role).WithMany(r => r.Accounts);
            HasOptional<Department>(a => a.Department).WithMany(r => r.Accounts);
        }
    }
}
