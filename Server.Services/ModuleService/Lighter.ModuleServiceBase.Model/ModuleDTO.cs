using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Lighter.ModuleServiceBase.Model
{
    [Description("模块信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class ModuleDTO : DTOEntityBase<string>
    {
        public ModuleDTO()
            : base(typeof(ModuleDTO))
        {

        }

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
            sb.Append(";Name: " + Name);
            sb.Append(";Catalog: " + Catalog ?? "<null>");

            return sb.ToString();
        }
    }
}
