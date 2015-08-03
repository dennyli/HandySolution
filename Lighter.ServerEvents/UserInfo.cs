using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.ServerEvents
{
    public class UserInfo
    {
        public UserInfo(string id, string name, string ip, string authority)
        {
            Id = id;
            Name = name;
            IP = ip;
            Authority = authority;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string IP { get; private set; }
        public string Authority { get; private set; }

        public override string ToString()
        {
            return Id + "|" + Name + "|" + (Authority == null ? "" : Authority);
        }
    }
}
