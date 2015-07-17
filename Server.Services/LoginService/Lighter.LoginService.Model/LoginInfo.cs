using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Lighter.LoginService.Model
{
    [DataContract]
    public class LoginInfo
    {
        public LoginInfo(string account, string password, string ipaddress)
        {
            Account = account;
            Password = password;
            IpAddress = ipaddress;
        }

        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string IpAddress { get; set; }
    }
}
