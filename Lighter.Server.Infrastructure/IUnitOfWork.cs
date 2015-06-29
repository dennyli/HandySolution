using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Server.Infrastructure
{
    public interface IUnitOfWork
    {
        bool Commit(out string error_message);
    }
}
