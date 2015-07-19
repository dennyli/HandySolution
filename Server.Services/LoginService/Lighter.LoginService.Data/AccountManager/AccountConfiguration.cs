using System;
using System.ComponentModel.Composition;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Lighter.Server.Infrastructure;

namespace Lighter.LoginService.Data.AccountManager
{
    [Export(typeof(IEntityMapper))]
    public class AccountConfiguration : EntityTypeConfiguration<Account>, IEntityMapper
    {
        public AccountConfiguration()
        {
        }

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            try
            {
                configurations.Add(this);
            }
            catch(InvalidOperationException)
            {

            }
        }
    }
}
