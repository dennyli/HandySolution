using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Lighter.ModuleServiceBase.Data
{
    [Description("模块信息")]
    [DataContract]
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
    }
}
