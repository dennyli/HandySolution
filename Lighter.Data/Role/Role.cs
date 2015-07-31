using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lighter.Data
{
    [Description("角色信息")]
    [DataContract]
    public class Role : EntityBase<string>
    {
        public Role()
        {
            Accounts = new ObservableCollection<Account>();
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

        //public string DepartCode { get; set; }
        //public virtual Department Department { get; set; }

        /// <summary>
        /// 拥有此角色的账户列表
        /// </summary>
        [DataMember]
        public virtual ICollection<Account> Accounts { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(";Name:" + Name);
            sb.Append(";Description:" + Description);
            //sb.Append(";DepartCode:" + Department.Id);

            return sb.ToString();
        }
    }
}
