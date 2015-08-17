using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Lighter.ModuleServiceBase.Model;
using System.ServiceModel;
using System.Text;
using Lighter.Data.Dto2Entity;

namespace Lighter.UserManagerService.Model
{
    [Description("角色信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class RoleDTO : DTOEntityBase<string>
    {
        public RoleDTO()
        {
            Accounts = new ObservableCollection<AccountDTO>();
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 角色权限
        /// </summary>
        [DataMember]
        public string Authority { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// 拥有此角色的账户列表
        /// </summary>
        [DataMember]
        public virtual Collection<AccountDTO> Accounts { get; set; }

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
                foreach (AccountDTO dto in Accounts)
                    sb.Append("\n\n\t" + dto.ToString());
            }

            return sb.ToString();
        }
    }
}
