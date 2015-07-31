using System.ComponentModel.Composition;
using Lighter.Server.Infrastructure;

namespace Lighter.Data.Configurations
{
    public partial class AccountConfiguration : LighterEntityConfiguration<Account, string>
    {
        public override void  LighterEntityConfigurationAppend()
        {
            HasOptional<Role>(a => a.Role).WithMany(r => r.Accounts);
            HasOptional<Department>(a => a.Department).WithMany(r => r.Accounts);
        }
    }
}
