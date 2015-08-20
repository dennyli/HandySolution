using System.Collections.Generic;
using System.ComponentModel.Composition;
using AutoMapper;
using Lighter.Data;
using Lighter.Data.Dto2Entity;
using Lighter.ModuleServiceBase.DtoMapping;
using Lighter.UserManagerService.Model;
using System.Collections.ObjectModel;
using System.Linq;
using Lighter.UserManagerService.UserManagerData;

namespace Lighter.UserManagerService.DtoMapping
{
    [Export(typeof(IUserMangerServiceDtoMapping))]
    public class UserMangerServiceDtoMapping : ModuleServiceBaseDtoMapping, IUserMangerServiceDtoMapping
    {
        [Import]
        public IUserManagerDataService _dataService { get; set; }

        public override void DtoMappingInitialize()
        {
            base.DtoMappingInitialize();

            // Entity to DTO
            TypeMap tm = Mapper.FindTypeMapFor<EntityBase<string>, DTOEntityBase<string>>();
            if (tm != null)
            {
                tm.IncludeDerivedTypes(typeof(Account), typeof(AccountDTO));
                tm.IncludeDerivedTypes(typeof(Department), typeof(DepartmentDTO));
                tm.IncludeDerivedTypes(typeof(Role), typeof(RoleDTO));
            }
            else
            {
                Mapper.CreateMap<EntityBase<string>, DTOEntityBase<string>>()
                    .Include<Account, AccountDTO>()
                    .Include<Department, DepartmentDTO>()
                    .Include<Role, RoleDTO>();
            }

            Mapper.CreateMap<Account, AccountDTO>()
                .ForMember(dto => dto.RoleName, opt => opt.MapFrom<string>(src => (src.Role == null ? null : src.Role.Name)))
                .ForMember(dto => dto.DepartmentName, opt => opt.MapFrom<string>(src => (src.Department == null ? null : src.Department.Name)));

            Mapper.CreateMap<Department, DepartmentDTO>()
                .ForMember(dto => dto.Accounts, opt => opt.MapFrom<ICollection<string>>(
                    src => (src.Accounts == null ? null : src.Accounts.Select<Account, string>(acc => acc.Id).ToList<string>())));

            Mapper.CreateMap<Role, RoleDTO>()
                .ForMember(dto => dto.Accounts, opt => opt.MapFrom<ICollection<string>>(
                    src => (src.Accounts == null ? null : src.Accounts.Select<Account, string>(acc => acc.Id).ToList<string>())));

            // DTO to Entity
            tm = Mapper.FindTypeMapFor<DTOEntityBase<string>, EntityBase<string>>();
            if (tm != null)
                tm.IncludeDerivedTypes(typeof(AccountDTO), typeof(Account));
            else
            {
                Mapper.CreateMap<DTOEntityBase<string>, EntityBase<string>>()
                    .Include<AccountDTO, Account>()
                    .Include<DepartmentDTO, Department>()
                    .Include<RoleDTO, Role>();
            }

            Mapper.CreateMap<AccountDTO, Account>()
                .ForMember(acc => acc.Role, opt => LoadEntity<AccountDTO, Role, string>(opt,
                    dto => dto.RoleId, _dataService.GetRoleById))
                .ForMember(acc => acc.Department, opt => LoadEntity<AccountDTO, Department, string>(opt,
                    dto => dto.DepartmentId, _dataService.GetDepartmentById))
                .ForMember(acc => acc.Role.Name, opt => opt.MapFrom(dto => dto.RoleName))
                .ForMember(acc => acc.Department.Name, opt => opt.MapFrom(dto => dto.DepartmentName));

            Mapper.CreateMap<DepartmentDTO, Department>()
                .ForMember(d => d.Accounts, opt => LoadEntity<DepartmentDTO, ICollection<Account>, string>(opt,
                    dto => dto.Id, _dataService.GetAccountsByDepartmentId));

            Mapper.CreateMap<RoleDTO, Role>()
                .ForMember(d => d.Accounts, opt => LoadEntity<RoleDTO, ICollection<Account>, string>(opt,
                    dto => dto.Id, _dataService.GetAccountsByRoleId));
        }
    }
}
