using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lighter.Data.Job
{
    public class Job : EntityBase<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string DepartCode { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(";Name:" + Name);
            sb.Append(";Description:" + Description);
            sb.Append(";DepartCode:" + DepartCode);

            return sb.ToString();
        }
    }
}
