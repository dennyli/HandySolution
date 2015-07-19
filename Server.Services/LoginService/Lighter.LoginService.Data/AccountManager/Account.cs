using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lighter.LoginService.Data.AccountManager
{
    [Table("Account")]
    public class Account : EntityBase<Int32>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Modules { get; set; }

        public string ShortName { get; set; }
        public string Code { get; set; }
        public string gwcode { get; set; }
        public string DepartCode { get; set; }
        public string loadflag { get; set; }
        public Nullable<Int32> khxh { get; set; }
        public bool IsLogin { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString() + ";");
            sb.Append(Name + ";");
            sb.Append(Password + ";");
            sb.Append(Modules + ";");
            sb.Append(Code + ";");
            sb.Append(gwcode + ";");
            sb.Append(DepartCode + ";");
            sb.Append(loadflag + ";");
            sb.Append(IsLogin + ";");
            sb.Append(khxh);

            return sb.ToString();
        }
    }
}
