using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lighter.LoginService.Data.AccountManager
{
    [Table("t_ygda")]
    public class Account : EntityBase<Int32>
    {
        [Column("ygmc")]
        public string Name { get; set; }
        [Column("mm")]
        public string Password { get; set; }
        [Column("qx")]
        public string Modules { get; set; }

        public string zjf { get; set; }
        public string ygbh { get; set; }
        public string gwcode { get; set; }
        public string bmbh { get; set; }
        public string loadflag { get; set; }
        public int khxh { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name + ";");
            sb.Append(Password + ";");
            sb.Append(Modules + ";");
            sb.Append(ygbh + ";");
            sb.Append(gwcode + ";");
            sb.Append(bmbh + ";");
            sb.Append(loadflag + ";");
            sb.Append(khxh);

            return sb.ToString();
        }
    }
}
