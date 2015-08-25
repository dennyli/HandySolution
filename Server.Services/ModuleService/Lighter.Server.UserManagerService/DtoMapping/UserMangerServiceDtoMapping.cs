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
using AutoMapper.Impl;

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
                if (!tm.IncludedDerivedTypes.Contains<TypePair>(new TypePair(typeof(Account), typeof(AccountDTO))))
                    tm.IncludeDerivedTypes(typeof(Account), typeof(AccountDTO));
                if (!tm.IncludedDerivedTypes.Contains<TypePair>(new TypePair(typeof(Department), typeof(DepartmentDTO))))
                    tm.IncludeDerivedTypes(typeof(Department), typeof(DepartmentDTO));
                if (!tm.IncludedDerivedTypes.Contains<TypePair>(new TypePair(typeof(Role), typeof(RoleDTO))))
                    tm.IncludeDerivedTypes(typeof(Role), typeof(RoleDTO));
            }
            else
            {
                Mapper.CreateMap<EntityBase<string>, DTOEntityBase<string>>()
                    .Include<Account, AccountDTO>()
                    .Include<Department, DepartmentDTO>()
                    .Include<Role, RoleDTO>();
            }

            tm = Mapper.FindTypeMapFor<Account, AccountDTO>();
            if (tm == null)
            {
                Mapper.CreateMap<Account, AccountDTO>();
                    //.ForMember(dto => dto.RoleName, opt => opt.MapFrom<string>(src => (src.Role == null ? null : src.Role.Name)))
                    //.ForMember(dto => dto.DepartmentName, opt => opt.MapFrom<string>(src => (src.Department == null ? null : src.Department.Name)));
                    //.AfterMap((acc, dto) =>
                    //{
                    //    if (acc.Role != null)
                    //        dto.RoleName = acc.Role.Name;
                    //    if (acc.Department != null)
                    //        dto.DepartmentName = acc.Department.Name;
                    //});
            }

            tm = Mapper.FindTypeMapFor<Department, DepartmentDTO>();
            if (tm == null)
            {
                Mapper.CreateMap<Department, DepartmentDTO>()
                   .ForMember(dto => dto.Accounts, opt => opt.MapFrom<ICollection<string>>(
                       src => (src.Accounts == null ? default(ICollection<string>)
                           : src.Accounts.Select<Account, string>(acc => acc.Id).ToList<string>())));
            }

            tm = Mapper.FindTypeMapFor<Role, RoleDTO>();
            if (tm == null)
            {
                Mapper.CreateMap<Role, RoleDTO>()
                   .ForMember(dto => dto.Accounts, opt => opt.MapFrom<ICollection<string>>(
                       src => (src.Accounts == null ? default(ICollection<string>)
                           : src.Accounts.Select<Account, string>(acc => acc.Id).ToList<string>())));
            }

            // DTO to Entity
            tm = Mapper.FindTypeMapFor<DTOEntityBase<string>, EntityBase<string>>();
            if (tm != null)
            {
                if (!tm.IncludedDerivedTypes.Contains<TypePair>(new TypePair(typeof(AccountDTO), typeof(Account))))
                    tm.IncludeDerivedTypes(typeof(AccountDTO), typeof(Account));
                if (!tm.IncludedDerivedTypes.Contains<TypePair>(new TypePair(typeof(DepartmentDTO), typeof(Department))))
                    tm.IncludeDerivedTypes(typeof(DepartmentDTO), typeof(Department));
                if (!tm.IncludedDerivedTypes.Contains<TypePair>(new TypePair(typeof(RoleDTO), typeof(Role))))
                    tm.IncludeDerivedTypes(typeof(RoleDTO), typeof(Role));
            }
            else
            {
                Mapper.CreateMap<DTOEntityBase<string>, EntityBase<string>>()
                    .Include<AccountDTO, Account>()
                    .Include<DepartmentDTO, Department>()
                    .Include<RoleDTO, Role>()
                    .ForMember(entity => entity.LastDate, opt => opt.Ignore());
            }

            tm = Mapper.FindTypeMapFor<AccountDTO, Account>();
            if (tm == null)
            {
                Mapper.CreateMap<AccountDTO, Account>()
                    .ForMember(acc => acc.LastDate, opt => opt.Ignore())
                    .ForMember(acc => acc.IsLogin, opt => opt.Ignore())
                    .ForMember(acc => acc.Role, opt => LoadEntity<AccountDTO, Role, string>(opt,
                        dto => dto.RoleId, _dataService.GetRoleById))
                    .ForMember(acc => acc.Department, opt => LoadEntity<AccountDTO, Department, string>(opt,
                        dto => dto.DepartmentId, _dataService.GetDepartmentById));
                    //.ForMember(acc => acc.Role.Name, opt => opt.MapFrom(dto => dto.RoleName))
                    //.ForMember(acc => acc.Department.Name, opt => opt.MapFrom(dto => dto.DepartmentName));
            }

            tm = Mapper.FindTypeMapFor<DepartmentDTO, Department>();
            if (tm == null)
            {
                Mapper.CreateMap<DepartmentDTO, Department>()
                    .ForMember(depart => depart.LastDate, opt => opt.Ignore())
                    .ForMember(depart => depart.Accounts, opt => LoadEntity<DepartmentDTO, ICollection<Account>, string>(opt, 
dto => dto.Id, _dataService.GetAccountsByDepartmentId));
            }

            tm = Mapper.FindTypeMapFor<RoleDTO, Role>();
            if (tm == null)
            {
                Mapper.CreateMap<RoleDTO, Role>()
                    .ForMember(role => role.LastDate, opt => opt.Ignore())
                    .ForMember(role => role.Accounts, opt => LoadEntity<RoleDTO, ICollection<Account>, string>(opt,
                        dto => dto.Id, _dataService.GetAccountsByRoleId));
            }
        }
    }
}
