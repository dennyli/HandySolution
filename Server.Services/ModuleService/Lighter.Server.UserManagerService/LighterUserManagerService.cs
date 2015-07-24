using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using Lighter.ModuleServiceBase;
using Lighter.ServiceManager;
using Lighter.ServiceManager.Endpoints;
using Lighter.UserManagerService.Interface;
using Lighter.UserManagerService.Model;
using Lighter.UserManagerService.UserManagerData;
using System.Collections.ObjectModel;
using Utility;
using Lighter.ModuleServiceBase.Data;
using AutoMapper;
using Lighter.Data;

namespace Lighter.UserManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, Namespace = "http://www.codestar.com/")]
    [ExportService("LighterUserManagerService", typeof(LighterUserManagerService), /*typeof(ILighterLoginService),*/ 1), TcpEndpoint(40002)]
    public class LighterUserManagerService : LighterModuleServiceBase, ILighterUserManagerService
    {
        [Import]
        protected IUserManagerDataService _dataService;

        public override string GetServiceId()
        {
            return "UserManager";
        }

        public override IEnumerable<ModuleDTO> GetModules()
        {
            return new List<ModuleDTO>()
            {
                new ModuleDTO() { Id = "U01", Name="用户管理", Catalog="账户管理" },
                new ModuleDTO() { Id = "U02", Name="部门管理", Catalog="账户管理" },
                new ModuleDTO() { Id = "U03", Name="岗位管理", Catalog="账户管理" },
                new ModuleDTO() { Id = "U04", Name="权限管理", Catalog="账户管理" },
                new ModuleDTO() { Id = "U05", Name="模块管理", Catalog="账户管理" }
            };
        }

        public override void Initialize()
        {
            base.Initialize();

            TypeMap baseMap = Mapper.FindTypeMapFor<DTOEntityBase<string>, EntityBase<string>>();
            if (baseMap == null)
                Mapper.CreateMap<DTOEntityBase<string>, EntityBase<string>>();
                
            if (!baseMap.TypeHasBeenIncluded(typeof(AccountDTO), typeof(Account)))
                baseMap.IncludeDerivedTypes(typeof(AccountDTO), typeof(Account));
            if (!baseMap.TypeHasBeenIncluded(typeof(DepartmentDTO), typeof(Department)))
                baseMap.IncludeDerivedTypes(typeof(DepartmentDTO), typeof(Department));
            if (!baseMap.TypeHasBeenIncluded(typeof(RoleDTO), typeof(Role)))
                baseMap.IncludeDerivedTypes(typeof(RoleDTO), typeof(Role));

            if (null == Mapper.FindTypeMapFor<AccountDTO, Account>())
                Mapper.CreateMap<AccountDTO, Account>();

            if (null == Mapper.FindTypeMapFor<DepartmentDTO, Department>())
                Mapper.CreateMap<DepartmentDTO, Department>();

            if (null == Mapper.FindTypeMapFor<RoleDTO, Role>())
                Mapper.CreateMap<RoleDTO, Role>();

            IsInitialized = true;
        }

        //public Collection<AccountDTO> GetAccounts()
        //{
        //    return _dataService.GetAccounts();
        //}

        //public Collection<AccountDTO> GetAccountsByDepartment(string departmentCode)
        //{
        //    return _dataService.GetAccountsByDepartment(departmentCode);
        //}

        //public Collection<AccountDTO> GetAccountsByRole(string roleCode)
        //{
        //    return _dataService.GetAccountsByRole(roleCode);
        //}

        //public OperationResult AddAccount(AccountDTO account)
        //{
        //    return _dataService.AddAccount(account);
        //}

        //public OperationResult AddAccounts(Collection<AccountDTO> accounts)
        //{
        //    return _dataService.AddAccounts(accounts);
        //}

        //public OperationResult UpdateAccount(AccountDTO account)
        //{
        //    return _dataService.UpdateAccount(account);
        //}

        //public OperationResult UpdateAccounts(Collection<AccountDTO> accounts)
        //{
        //    return _dataService.UpdateAccounts(accounts);
        //}

        //public OperationResult DeleteAccount(string accountCode, bool bRemoveRecord)
        //{
        //    return _dataService.DeleteAccount(accountCode, bRemoveRecord);
        //}

        //public OperationResult DeleteAccounts(Collection<string> accountCodes, bool bRemoveRecord)
        //{
        //    return _dataService.DeleteAccounts(accountCodes, bRemoveRecord);
        //}

        //public Collection<RoleDTO> GetRoles()
        //{
        //    return _dataService.GetRoles();
        //}

        //public RoleDTO GetRole(string roleCode)
        //{
        //    return _dataService.GetRole(roleCode);
        //}

        //public OperationResult AddRole(RoleDTO role)
        //{
        //    return _dataService.AddRole(role);
        //}

        //public OperationResult AddRoles(Collection<RoleDTO> roles)
        //{
        //    return _dataService.AddRoles();
        //}

        //public OperationResult UpdateRole(RoleDTO role)
        //{
        //    return _dataService.UpdateRole();
        //}

        //public OperationResult UpdateRoles(Collection<RoleDTO> roles)
        //{
        //    return _dataService.UpdateRoles();
        //}

        //public OperationResult DeleteRole(string roleCode, bool bRemoveRecord)
        //{
        //    return _dataService.DeleteRole();
        //}

        //public OperationResult DeleteRoles(Collection<string> roleCodes, bool bRemoveRecord)
        //{
        //    return _dataService.DeleteRoles();
        //}

        //public Collection<DepartmentDTO> GetDepartments()
        //{
        //    return _dataService.GetDepartments();
        //}

        //public DepartmentDTO GetDepartment(string departmentCode)
        //{
        //    return _dataService.GetDepartment();
        //}

        //public OperationResult AddDepartment(DepartmentDTO department)
        //{
        //    return _dataService.AddDepartment();
        //}

        //public OperationResult AddDepartments(Collection<DepartmentDTO> departments)
        //{
        //    return _dataService.AddDepartments();
        //}

        //public OperationResult UpdateDepartment(DepartmentDTO department)
        //{
        //    return _dataService.UpdateDepartment();
        //}

        //public OperationResult UpdateDepartments(Collection<DepartmentDTO> departments)
        //{
        //    return _dataService.UpdateDepartments();
        //}

        //public OperationResult DeleteDepartment(string departmentCode, bool bRemoveRecord)
        //{
        //    return _dataService.DeleteDepartment();
        //}

        //public OperationResult DeleteDepartments(Collection<string> departmentCodes, bool bRemoveRecord)
        //{
        //    return _dataService.DeleteDepartments();
        //}
    }
}
