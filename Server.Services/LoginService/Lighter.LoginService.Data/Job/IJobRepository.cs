using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Server.Infrastructure;

namespace Lighter.Data.Job
{
    public interface IJobRepository : IRepository<Job, string>
    {
    }
}
