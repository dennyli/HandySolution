using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Lighter.ModuleServiceBase.Model;
using System.ServiceModel;
using System.Text;

namespace Lighter.UserManagerService.Model
{
    [Description("部门信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class DepartmentDTO : DTOEntityBase<string>
    {
        public DepartmentDTO()
        {
            Accounts = new ObservableCollection<AccountDTO>();
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 此部门的账户列表
        /// </summary>
        [DataMember]
        public virtual Collection<AccountDTO> Accounts { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("\nName: " + Name);
            sb.Append("\nDescription: " + Description ?? "<null>");
            if (Accounts ==null)
                sb.Append("\nAccounts: <null>");
            else
            {
                sb.Append("\nAccounts: " + Accounts.Count.ToString() + " account");
                foreach(AccountDTO dto in Accounts)
                    sb.Append("\n\n\t" + dto.ToString());
            }

            return sb.ToString();
        }
    }
}
