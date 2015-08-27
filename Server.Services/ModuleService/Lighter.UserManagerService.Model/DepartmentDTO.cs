using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Lighter.ModuleServiceBase.Model;
using System.ServiceModel;
using System.Text;
using Lighter.Data.Dto2Entity;
using System.Collections.Generic;

namespace Lighter.UserManagerService.Model
{
    [Description("部门信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class DepartmentDTO : DTOEntityBase<string>
    {
        public DepartmentDTO()
        {
            //Accounts = new ObservableCollection<string>();
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public string Name
        {
            get
            {
                return GetPropertyValue<string>(
#if NET45
#else
"Name"
#endif
);
            }
            set
            {
                SetPropertyValue<string>(value
#if NET45
#else
, "Name"
#endif
);
            }
        }

        /// <summary>
        /// 部门描述
        /// </summary>
        [DataMember]
        public string Description
        {
            get
            {
                return GetPropertyValue<string>(
#if NET45
#else
"Description"
#endif
);
            }
            set
            {
                SetPropertyValue<string>(value
#if NET45
#else
, "Description"
#endif
);
            }
        }

        ///// <summary>
        ///// 此部门的账户列表
        ///// </summary>
        [DataMember]
        public virtual ICollection<string> Accounts
        {
            get
            {
                return GetPropertyValue<ICollection<string>>(
#if NET45
#else
"Accounts"
#endif
);
            }
            set
            {
                SetPropertyValue<ICollection<string>>(value
#if NET45
#else
, "Accounts"
#endif
);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("\nName: " + Name);
            sb.Append("\nDescription: " + Description ?? "<null>");
            if (Accounts == null)
                sb.Append("\nAccounts: <null>");
            else
            {
                sb.Append("\nAccounts: " + Accounts.Count.ToString() + " account");
                //foreach (AccountDTO dto in Accounts)
                //    sb.Append("\n\n\t" + dto.ToString());

                foreach (string id in Accounts)
                    sb.Append("\n\n\t" + id);
            }

            return sb.ToString();
        }
    }
}
