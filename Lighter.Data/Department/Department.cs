﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Lighter.Data
{
    [Description("部门信息")]
    public class Department : EntityBase<string>
    {
        public Department()
        {
            Accounts = new ObservableCollection<Account>();
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string Description { get; set; }

        //public virtual ICollection<Role> Accounts { get; set; }

        /// <summary>
        /// 此部门的账户列表
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(";Name:" + Name);
            sb.Append(";Description:" + Description);

            return sb.ToString();
        }
    }
}
