using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Server.Infrastructure
{
    public interface IEFDbContextProvider
    {
        string GetConnectionString();

        void SetConnectionParameter(string providerName, string serverName, string dbName);
    }
}
