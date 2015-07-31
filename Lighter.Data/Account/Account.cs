using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Lighter.Data
{
    [Description("账户信息")]
    [DataContract]
    public class Account : EntityBase<string>
    {
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
        /// 账户角色ID
        /// </summary>
        [DataMember]
        [ForeignKey("Role")]
        public string RoleId { get; set; }

        /// <summary>
        /// 账户角色
        /// </summary>
        [DataMember]
        public virtual Role Role { get; set; }
        
        /// <summary>
        /// 账户所属部门ID
        /// </summary>
        [DataMember]
        [ForeignKey("Department")]
        public string DepartId { get; set; }

        /// <summary>
        /// 账户所属部门
        /// </summary>
        [DataMember]
        public virtual Department Department { get; set; }

        /// <summary>
        /// 是否已登录标记
        /// </summary>
        [DataMember]
        public bool IsLogin { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(";Name:" + Name);
            sb.Append(";Password:" + Password);
            sb.Append(";Authority:" + Authority == null ? "" : Authority);
            sb.Append(";RoleId:" + Role == null ? "" : Role.Id);
            sb.Append(";DepartId:" + Department == null ? "" : Department.Id);
            sb.Append(";IsLogin:" + IsLogin.ToString());

            return sb.ToString();
        }
    }
}
