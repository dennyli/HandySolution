using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Lighter.Server.Infrastructure
{
    public interface IDbContextFactory<TContext> where TContext : DbContext, new()
    {
        TContext GetDbContext();
    }
}
