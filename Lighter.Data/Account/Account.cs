using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lighter.Data
{
    [Description("账户信息")]
    public class Account : EntityBase<string>
    {
        /// <summary>
        /// 账户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 账户权限
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// 账户名称缩写
        /// </summary>
        public string ShortName { get; set; }

        //public string RoleCode { get; set; }
        //public string DepartCode { get; set; }

        /// <summary>
        /// 账户角色
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 账户所属部门
        /// </summary>
        public virtual Department Department { get; set; }

        public bool IsLogin { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(";Name:" + Name);
            sb.Append(";Password:" + Password);
            sb.Append(";Authority:" + Authority);
            sb.Append(";RoleCode:" + Role.Id);
            sb.Append(";DepartCode:" + Department.Id);
            sb.Append(";IsLogin:" + IsLogin.ToString());

            return sb.ToString();
        }
    }
}
