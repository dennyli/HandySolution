using System.ComponentModel.Composition;
using AutoMapper;
using Lighter.Data;
using Lighter.Data.Dto2Entity;
using Lighter.ModuleServiceBase.DtoMapping;
using Lighter.UserManagerService.Model;

namespace Lighter.UserManagerService.DtoMapping
{
    [Export(typeof(IUserMangerServiceDtoMapping))]
    public class UserMangerServiceDtoMapping : ModuleServiceBaseDtoMapping, IUserMangerServiceDtoMapping
    {
        public override void DtoMappingInitialize()
        {
            base.DtoMappingInitialize();

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

            Mapper.CreateMap<Account, AccountDTO>();
            Mapper.CreateMap<Department, DepartmentDTO>();
            Mapper.CreateMap<Role, RoleDTO>();
        }
    }
}
