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
    [DataContract(Namespace = "http://www.codestar.com/")]
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
        public string Name
        {
            get { return GetPropertyValue<string>(
                #if NET45
#else
"Name"
#endif
); }
            set { SetPropertyValue<string>(value
                 #if NET45
#else
, "Name"
#endif
); }
        }

        /// <summary>
        /// 模块类别
        /// </summary>
        [DataMember]
        public string Catalog
        {
            get { return GetPropertyValue<string>(
                #if NET45
#else
"Catalog"
#endif
); }
            set { SetPropertyValue<string>(value
                 #if NET45
#else
, "Catalog"
#endif
); }
        }

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
