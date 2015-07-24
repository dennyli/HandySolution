using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel;

namespace Lighter.Data
{
    [Description("模块信息")]
    public class Module : EntityBase<string>
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模块类别
        /// </summary>
        public string Catalog { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(";Catalog:" + Catalog);
            sb.Append(";Name:" + Name);

            return sb.ToString();
        }
    }
}
