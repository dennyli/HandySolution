using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Client.Infrastructure.Interface
{
    public interface IConfigContext
    {
        string GetClientName();

        string GetServerName();
        string GetServerIp();
        string GetServerPort();

        void WriteServerName(string name);
        void WriteServerIp(string ip);
        void WriteServerPort(string port); 
    }
}
