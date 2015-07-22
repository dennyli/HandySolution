using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Server.Infrastructure;
using System.ComponentModel.Composition;

namespace Lighter.Data.Job
{
    [Export(typeof(IJobRepository))]
    public class JobRepository : EFRepositoryBase<Job, string>, IJobRepository
    {
    }
}
