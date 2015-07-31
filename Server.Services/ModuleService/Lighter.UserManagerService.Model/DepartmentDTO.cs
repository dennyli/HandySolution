using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Lighter.ModuleServiceBase.Model;
using System.ServiceModel;

namespace Lighter.UserManagerService.Model
{
    [Description("部门信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class DepartmentDTO : DTOEntityBase<string>
    {
        public DepartmentDTO()
            : base(typeof(DepartmentDTO))
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
    }
}
