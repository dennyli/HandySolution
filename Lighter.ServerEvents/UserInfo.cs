using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.ServerEvents
{
    public class UserInfo
    {
        public UserInfo(string name, string ip)
        {
            Name = name;
            IP = ip;
        }

        public string Name { get; private set; }
        public string IP { get; private set; }
    }
}
