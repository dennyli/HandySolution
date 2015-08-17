using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using Lighter.Data.Dto2Entity;

namespace Lighter.Data
{
    [Description("模块信息")]
    [DataContract]
    public class Module : EntityBase<string>
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 模块类别
        /// </summary>
        [DataMember]
        public string Catalog { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("\nCatalog:" + Catalog);
            sb.Append("\nName:" + Name);

            return sb.ToString();
        }
    }
}
