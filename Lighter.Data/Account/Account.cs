using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using Lighter.Data.Dto2Entity;

namespace Lighter.Data
{
    [Description("账户信息")]
    [DataContract]
    public class Account : EntityBase<string>//, IDTO2EntityConvertor<string>
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
            sb.Append("\nName:" + Name);
            sb.Append("\nPassword:" + Password);
            sb.Append("\nAuthority:" + (Authority == null ? "" : Authority));
            sb.Append("\nRoleId:" + (Role == null ? "" : Role.Id));
            sb.Append("\nDepartId:" + (Department == null ? "" : Department.Id));
            sb.Append("\nIsLogin:" + IsLogin.ToString());

            return sb.ToString();
        }

        //#region IDTO2EntityConvertor<string> 成员

        //public void FromDto(DTOEntityBase<string> entity)
        //{
        //    //PublicHelper.CheckArgumentIsType(entity, "entity", typeof(AccountDTO));

        //    //this.Id = entity.Id;

        //    //AccountDTO dto = entity as AccountDTO;
        //    //this.Name = dto.Name;
        //    //this.Authority = dto.Authority;

        //    //if (dto.Department != null)
        //    //{
        //    //    this.DepartId = dto.Department.Id;
        //    //    this.Department = new Department();
        //    //    this.Department.FromDto(dto.Department);
        //    //}

        //    //this.Password = dto.Password;

        //    //if (dto.Role != null)
        //    //{
        //    //    this.RoleId = dto.Role.Id;
        //    //    this.Role = new Role();
        //    //    this.Role.FromDto(dto.Role);
        //    //}

        //    //this.ShortName = dto.ShortName;
        //}

        //public DTOEntityBase<string> ToDto()
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion
    }
}
