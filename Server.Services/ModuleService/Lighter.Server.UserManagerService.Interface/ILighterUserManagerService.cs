using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.ModuleServiceBase;
using System.ServiceModel;
using Lighter.UserManagerService.Model;
using System.Collections.ObjectModel;
using Utility;
using Lighter.ModuleServiceBase.Data;

namespace Lighter.UserManagerService.Interface
{
    [ServiceContract(SessionMode = SessionMode.Required, Namespace = "http://www.codestar.com/")]
    [ServiceKnownType(typeof(AccountDTO))]
    [ServiceKnownType(typeof(DepartmentDTO))]
    [ServiceKnownType(typeof(RoleDTO))]
    [ServiceKnownType(typeof(ModuleDTO))]
    public interface ILighterUserManagerService : ILighterModuleService
    {
        //#region Account
        //Collection<AccountDTO> GetAccounts();
        //Collection<AccountDTO> GetAccountsByDepartment(string departmentCode);
        //Collection<AccountDTO> GetAccountsByRole(string roleCode);

        //OperationResult AddAccount(AccountDTO account);
        //OperationResult AddAccounts(Collection<AccountDTO> accounts);

        //OperationResult UpdateAccount(AccountDTO account);
        //OperationResult UpdateAccounts(Collection<AccountDTO> accounts);

        //OperationResult DeleteAccount(string accountCode, bool bRemoveRecord);
        //OperationResult DeleteAccounts(Collection<string> accountCodes, bool bRemoveRecord);
        //#endregion

        //#region Role
        //Collection<RoleDTO> GetRoles();
        //RoleDTO GetRole(string roleCode);

        //OperationResult AddRole(RoleDTO role);
        //OperationResult AddRoles(Collection<RoleDTO> roles);

        //OperationResult UpdateRole(RoleDTO role);
        //OperationResult UpdateRoles(Collection<RoleDTO> roles);

        //OperationResult DeleteRole(string roleCode, bool bRemoveRecord);
        //OperationResult DeleteRoles(Collection<string> roleCodes, bool bRemoveRecord);
        //#endregion

        //#region Department
        //Collection<DepartmentDTO> GetDepartments();
        //DepartmentDTO GetDepartment(string departmentCode);

        //OperationResult AddDepartment(DepartmentDTO department);
        //OperationResult AddDepartments(Collection<DepartmentDTO> departments);

        //OperationResult UpdateDepartment(DepartmentDTO department);
        //OperationResult UpdateDepartments(Collection<DepartmentDTO> departments);

        //OperationResult DeleteDepartment(string departmentCode, bool bRemoveRecord);
        //OperationResult DeleteDepartments(Collection<string> departmentCodes, bool bRemoveRecord);
        //#endregion
    }
}
