using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Client.Infrastructure.Interface
{
    public interface IConfig
    {
        string GetServerName();
        string GetServerIp();
        string GetServerPort();
    }
}
