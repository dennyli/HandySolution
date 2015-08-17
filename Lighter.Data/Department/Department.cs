using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Lighter.Data.Dto2Entity;

namespace Lighter.Data
{
    [Description("部门信息")]
    [DataContract]
    public class Department : EntityBase<string>
    {
        public Department()
        {
            Accounts = new ObservableCollection<Account>();
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

        //public virtual ICollection<Role> Accounts { get; set; }

        /// <summary>
        /// 此部门的账户列表
        /// </summary>
        [DataMember]
        public virtual ICollection<Account> Accounts { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("\nName:" + Name);
            sb.Append("\nDescription:" + Description);

            return sb.ToString();
        }
    }
}
