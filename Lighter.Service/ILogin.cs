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
        void Disconnect(Client client);
    }
}
