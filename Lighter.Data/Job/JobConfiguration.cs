using System;
using System.ComponentModel.Composition;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Lighter.Server.Infrastructure;

namespace Lighter.Data.Job
{
    [Export(typeof(IEntityMapper))]
    public class JobConfiguration : EntityConfiguration<Job, string>
    {
        public JobConfiguration()
        {
        }
    }
}
