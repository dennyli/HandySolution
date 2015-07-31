using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Lighter.ModuleServiceBase.Model;
using System.ServiceModel;

namespace Lighter.UserManagerService.Model
{
    [Description("角色信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class RoleDTO : DTOEntityBase<string>
    {
        public RoleDTO()
            : base(typeof(RoleDTO))
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
    }
}
