using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lighter.Data.Department
{
    public class Department : EntityBase<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(";Name:" + Name);
            sb.Append(";Description:" + Description);

            return sb.ToString();
        }
    }
}
