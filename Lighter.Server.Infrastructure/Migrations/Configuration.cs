using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Migrations;
using System.ComponentModel.Composition;

namespace Lighter.Server.Infrastructure.Migrations
{
    public sealed class Configuration:DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        [ImportMany(typeof(IConfigurationExtension), RequiredCreationPolicy=CreationPolicy.Shared)]
        public ICollection<IConfigurationExtension> ConfigurationExtensions;

        protected override void Seed(EFDbContext context)
        {
            if (ConfigurationExtensions != null)
            {
                foreach (IConfigurationExtension ce in ConfigurationExtensions)
                {
                    ce.Initialize(context);
                }
            }
        }
    }
}
