using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Model;

namespace Lighter.Service
{
    public interface ILogin
    {
        bool Connect(Client client);
        bool Login();
        bool Logout();
        void Disconnect(Client client);
    }
}
