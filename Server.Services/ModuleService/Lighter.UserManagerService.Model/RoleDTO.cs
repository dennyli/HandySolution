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
    [Description("角色信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class RoleDTO : DTOEntityBase<string>
    {
        public RoleDTO()
        {
            //Accounts = new ObservableCollection<string>();
        }

        /// <summary>
        /// 角色名称
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
);
            }
        }

        /// <summary>
        /// 角色权限
        /// </summary>
        [DataMember]
        public string Authority
        {
            get { return GetPropertyValue<string>(
                                #if NET45
#else
"Authority"
#endif
); }
            set { SetPropertyValue<string>(value
                #if NET45
#else
, "Authority"
#endif
); }
        }

        /// <summary>
        /// 角色描述
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return GetPropertyValue<string>(
                #if NET45
#else
"Description"
#endif
); }
            set { SetPropertyValue<string>(value
                #if NET45
#else
, "Description"
#endif
); }
        }

        /// <summary>
        /// 拥有此角色的账户Id列表
        /// </summary>
        [DataMember]
        public virtual ICollection<string> Accounts
        {
            get { return GetPropertyValue<ICollection<string>>(
#if NET45
#else
"Accounts"
#endif
); }
            set { SetPropertyValue<ICollection<string>>(value
#if NET45
#else
, "Accounts"
#endif
); }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("\nName: " + Name);
            sb.Append("\nAuthority: " + Authority ?? "<null>");
            sb.Append("\nDescription: " + Description ?? "<null>");
            if (Accounts == null)
                sb.Append("\nAccounts: <null>");
            else
            {
                sb.Append("\nAccounts: " + Accounts.Count.ToString() + " account");

                foreach (string id in Accounts)
                    sb.Append("\n\n\t" + id);
            }

            return sb.ToString();
        }
    }
}
