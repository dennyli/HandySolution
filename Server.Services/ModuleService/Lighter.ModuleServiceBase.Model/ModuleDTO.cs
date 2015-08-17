using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using Lighter.Data.Dto2Entity;

namespace Lighter.ModuleServiceBase.Model
{
    [Description("模块信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class ModuleDTO : DTOEntityBase<string>
    {
        public ModuleDTO()
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
            sb.Append("\nName: " + Name);
            sb.Append("\nCatalog: " + Catalog ?? "<null>");

            return sb.ToString();
        }
    }
}
