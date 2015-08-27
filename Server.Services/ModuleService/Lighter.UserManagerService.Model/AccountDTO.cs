using System.ComponentModel;
using System.Runtime.Serialization;
using Lighter.ModuleServiceBase.Model;
using System.ServiceModel;
using System.Text;
using Lighter.Data.Dto2Entity;
using System;

namespace Lighter.UserManagerService.Model
{
    [Description("账户信息")]
    [DataContract]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class AccountDTO : DTOEntityBase<string>
    {
        public AccountDTO()
        {

        }

        /// <summary>
        /// 账户名称
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return GetPropertyValue<string>(
#if NET45
#else
                    "Name"
#endif
                    ); }
            set { SetPropertyValue<string>(value
#if NET45
#else
, "Name"
#endif
);
            }
        }

        /// <summary>
        /// 账户密码
        /// </summary>
        [DataMember]
        public string Password
        {
            get { return GetPropertyValue<string>(
#if NET45
#else
            "Password"
#endif
);
            }
            set { SetPropertyValue<string>(value
#if NET45
#else
, "Password"
#endif
);
            }
        }

        /// <summary>
        /// 账户权限
        /// </summary>
        [DataMember]
        public string Authority
        {
            get { return GetPropertyValue<string>(
                #if NET45
#else
"Authority"
#endif
            ); }
            set { SetPropertyValue<string>(value
#if NET45
#else
, "Authority"
#endif
);
            }
        }

        /// <summary>
        /// 账户名称缩写
        /// </summary>
        [DataMember]
        public string ShortName
        {
            get { return GetPropertyValue<string>(
#if NET45
#else
"ShortName"
#endif
            ); }
            set { SetPropertyValue<string>(value
#if NET45
#else
, "ShortName"
#endif
);
            }
        }

        ///// <summary>
        ///// 账户角色
        ///// </summary>
        //[DataMember]
        //public virtual RoleDTO Role { get; set; }

        /// <summary>
        /// 账户角色Id
        /// </summary>
        [DataMember]
        public string RoleId
        {
            get { return GetPropertyValue<string>(
#if NET45
#else
"RoleId"
#endif
);
            }
            set { SetPropertyValue<string>(value
#if NET45
#else
, "RoleId"
#endif
);
            }
        }

        ///// <summary>
        ///// 账户角色名字
        ///// </summary>
        //[DataMember]
        //public string RoleName { get; set; }

        ///// <summary>
        ///// 账户所属部门
        ///// </summary>
        //[DataMember]
        //public virtual DepartmentDTO Department { get; set; }

        /// <summary>
        /// 账户所属部门Id
        /// </summary>
        [DataMember]
        public string DepartmentId
        {
            get { return GetPropertyValue<string>(
#if NET45
#else
"DepartmentId"
#endif
);
            }
            set { SetPropertyValue<string>(value
#if NET45
#else
, "DepartmentId"
#endif
);
            }
        }

        ///// <summary>
        ///// 账户所属部门名字
        ///// </summary>
        //[DataMember]
        //public string DepartmentName { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("\nName: " + Name);
            sb.Append("\nPassword: " + Password ?? "<null>");
            sb.Append("\nAuthority: " + (Authority == null ? "<null>" : Authority));
            sb.Append("\nShortName: " + ShortName ?? "<null>");
            //sb.Append("\nRole: " + (Role == null ? "<null>" : Role.ToString()));
            //sb.Append("\nDepartment: " + (Department == null ? "<null>" : Department.ToString()));

            sb.Append("\nRoleId: " + (RoleId == null ? "<null>" : RoleId));
            //sb.Append("\nRoleName: " + (RoleName == null ? "<null>" : RoleName));
            sb.Append("\nDepartmentId: " + (DepartmentId == null ? "<null>" : DepartmentId));
            //sb.Append("\nDepartmentName: " + (DepartmentName == null ? "<null>" : DepartmentName));

            return sb.ToString();
        }
    }
}
