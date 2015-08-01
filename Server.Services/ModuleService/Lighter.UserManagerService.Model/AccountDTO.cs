using System.ComponentModel;
using System.Runtime.Serialization;
using Lighter.ModuleServiceBase.Model;
using System.ServiceModel;
using System.Text;

namespace Lighter.UserManagerService.Model
{
    [Description("账户信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class AccountDTO : DTOEntityBase<string>
    {
        public AccountDTO()
            : base(typeof(AccountDTO))
        {

        }

        /// <summary>
        /// 账户名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 账户密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// 账户权限
        /// </summary>
        [DataMember]
        public string Authority { get; set; }

        /// <summary>
        /// 账户名称缩写
        /// </summary>
        [DataMember]
        public string ShortName { get; set; }

        /// <summary>
        /// 账户角色
        /// </summary>
        [DataMember]
        public virtual RoleDTO Role { get; set; }

        /// <summary>
        /// 账户所属部门
        /// </summary>
        [DataMember]
        public virtual DepartmentDTO Department { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(";Name: " + Name);
            sb.Append(";Password: " + Password ?? "<null>");
            sb.Append(";Authority: " + Authority ?? "<null>");
            sb.Append(";ShortName: " + ShortName ?? "<null>");
            sb.Append(";Role: " + Role == null ? "<null>" : Role.ToString());
            sb.Append(";Department: " + Department == null ? "<null>" : Department.ToString());

            return sb.ToString();
        }
    }
}
