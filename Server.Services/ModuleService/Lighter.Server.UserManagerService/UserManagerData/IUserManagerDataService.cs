using Lighter.UserManagerService.Model;
using Utility;
using Lighter.ModuleServiceBase.Data;
using Lighter.ModuleServiceBase;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lighter.UserManagerService.UserManagerData
{
    [KnownType(typeof(AccountDTO))]
    [KnownType(typeof(DepartmentDTO))]
    [KnownType(typeof(RoleDTO))]
    [KnownType(typeof(ModuleDTO))]
    interface IUserManagerDataService : IModuleDataServiceBase
    {
        #region Generic
        DTOEntityBase<string> GetDTOEntity(string key, Type type);
        List<DTOEntityBase<string>> GetDTOEntities(Type type);

        OperationResult AddEntity(DTOEntityBase<string> entity);
        OperationResult AddEntities(List<DTOEntityBase<string>> entities);

        OperationResult UpdateEntity(DTOEntityBase<string> entity);
        OperationResult UpdateEntities(List<DTOEntityBase<string>> entities);

        OperationResult DeleteEntity(DTOEntityBase<string> entity, bool bRemoveRecord);
        OperationResult DeleteEntities(List<DTOEntityBase<string>> entities, bool bRemoveRecord);
        #endregion 

        //#region Account
        //List<AccountDTO> GetAccounts();
        //List<AccountDTO> GetAccountsByDepartment(string departmentCode);
        //List<AccountDTO> GetAccountsByRole(string roleCode);

        //OperationResult AddAccount(AccountDTO account);
        //OperationResult AddAccounts(List<AccountDTO> accounts);

        //OperationResult UpdateAccount(AccountDTO account);
        //OperationResult UpdateAccounts(List<AccountDTO> accounts);

        //OperationResult DeleteAccount(string accountCode, bool bRemoveRecord);
        //OperationResult DeleteAccounts(List<string> accountCodes, bool bRemoveRecord);
        //#endregion

        //#region Role
        //List<RoleDTO> GetRoles();
        //RoleDTO GetRole(string roleCode);

        //OperationResult AddRole(RoleDTO role);
        //OperationResult AddRoles(List<RoleDTO> roles);

        //OperationResult UpdateRole(RoleDTO role);
        //OperationResult UpdateRoles(List<RoleDTO> roles);

        //OperationResult DeleteRole(string roleCode, bool bRemoveRecord);
        //OperationResult DeleteRoles(List<string> roleCodes, bool bRemoveRecord);
        //#endregion

        //#region Department
        //List<DepartmentDTO> GetDepartments();
        //DepartmentDTO GetDepartment(string departmentCode);

        //OperationResult AddDepartment(DepartmentDTO department);
        //OperationResult AddDepartments(List<DepartmentDTO> departments);

        //OperationResult UpdateDepartment(DepartmentDTO department);
        //OperationResult UpdateDepartments(List<DepartmentDTO> departments);

        //OperationResult DeleteDepartment(string departmentCode, bool bRemoveRecord);
        //OperationResult DeleteDepartments(List<string> departmentCodes, bool bRemoveRecord);
        //#endregion

    }
}
