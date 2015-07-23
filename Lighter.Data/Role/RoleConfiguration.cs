using System;
using System.ComponentModel.Composition;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Lighter.Server.Infrastructure;

namespace Lighter.Data
{
    [Export(typeof(IEntityMapper))]
    public class RoleConfiguration : EntityConfiguration<Role, string>
    {
        public RoleConfiguration()
        {
        }
    }
}
