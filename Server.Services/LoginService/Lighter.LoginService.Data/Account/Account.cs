using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lighter.Data.Account
{
    public class Account : EntityBase<string>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Authority { get; set; }

        public string ShortName { get; set; }

        public string JobCode { get; set; }
        public string DepartCode { get; set; }
        public bool IsLogin { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(";Name:" + Name);
            sb.Append(";Password:" + Password);
            sb.Append(";Authority:" + Authority);
            sb.Append(";JobCode:" + JobCode);
            sb.Append(";DepartCode:" + DepartCode);
            sb.Append(";IsLogin:" + IsLogin.ToString());

            return sb.ToString();
        }
    }
}
