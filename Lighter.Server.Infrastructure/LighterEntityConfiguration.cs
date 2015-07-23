using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Utility;

namespace Lighter.Server.Infrastructure
{
    public class LighterEntityConfiguration<TEntity, TKey> : EntityTypeConfiguration<TEntity>, IEntityMapper where TEntity : EntityBase<TKey>
    {
        public LighterEntityConfiguration()
        {
            this.Map(m => m.ToTable(typeof(TEntity).Name));
        }

        public virtual void RegistTo(ConfigurationRegistrar configurations)
        {
            try
            {
                configurations.Add(this);
            }
            catch (InvalidOperationException)
            {

            }
        }

        public virtual void LighterEntityConfigurationAppend() { }
    }
}
