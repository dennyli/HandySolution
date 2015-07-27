using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using Lighter.Data;
using Lighter.Data.Repositories;
using Lighter.ModuleServiceBase.Data;
using Lighter.ModuleServiceBase.Model;
using Lighter.UserManagerService.Model;
using Utility;

namespace Lighter.UserManagerService.UserManagerData
{
    [Export(typeof(IUserManagerDataService))]
    [KnownType(typeof(AccountDTO))]
    [KnownType(typeof(DepartmentDTO))]
    [KnownType(typeof(RoleDTO))]
    [KnownType(typeof(ModuleDTO))]
    public class UserManagerDataService : ModuleDataServiceBase, IUserManagerDataService
    {
        [Import]
        protected IRoleRepository RoleRepository { get; set; }

        public IQueryable<Role> Roles
        {
            get { return RoleRepository.Entities; }
        }

        [Import]
        protected IModuleRepository ModuleRepository { get; set; }

        public IQueryable<Module> Modules
        {
            get { return ModuleRepository.Entities; }
        }

        [Import]
        protected IDepartmentRepository DepartmentRepository { get; set; }

        public IQueryable<Department> Departments
        {
            get { return DepartmentRepository.Entities; }
        }

        #region Explict Declare
        //public List<AccountDTO> GetAccounts()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<AccountDTO> GetAccountsByDepartment(string departmentCode)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<AccountDTO> GetAccountsByRole(string roleCode)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult AddAccount(AccountDTO account)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult AddAccounts(List<AccountDTO> accounts)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult UpdateAccount(AccountDTO account)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult UpdateAccounts(List<AccountDTO> accounts)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult DeleteAccount(string accountCode, bool bRemoveRecord)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult DeleteAccounts(List<string> accountCodes, bool bRemoveRecord)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<RoleDTO> GetRoles()
        //{
        //    throw new NotImplementedException();
        //}

        //public RoleDTO GetRole(string roleCode)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult AddRole(RoleDTO role)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult AddRoles(List<RoleDTO> roles)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult UpdateRole(RoleDTO role)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult UpdateRoles(List<RoleDTO> roles)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult DeleteRole(string roleCode, bool bRemoveRecord)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult DeleteRoles(List<string> roleCodes, bool bRemoveRecord)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<DepartmentDTO> GetDepartments()
        //{
        //    throw new NotImplementedException();
        //}

        //public DepartmentDTO GetDepartment(string departmentCode)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult AddDepartment(DepartmentDTO department)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult AddDepartments(List<DepartmentDTO> departments)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult UpdateDepartment(DepartmentDTO department)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult UpdateDepartments(List<DepartmentDTO> departments)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult DeleteDepartment(string departmentCode, bool bRemoveRecord)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult DeleteDepartments(List<string> departmentCodes, bool bRemoveRecord)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

        #region Generic
        DTOEntityBase<string> IUserManagerDataService.GetDTOEntity(string key, Type type)
        {
            return null;
        }

        List<DTOEntityBase<string>> IUserManagerDataService.GetDTOEntities(Type type)
        {
            if (type == typeof(AccountDTO))
            {
                List<Account> accounts = Accounts.ToList<Account>();
            }

            return null;
        }

        OperationResult IUserManagerDataService.AddEntity(DTOEntityBase<string> entity)
        {
            throw new NotImplementedException();
        }

        OperationResult IUserManagerDataService.AddEntities(List<DTOEntityBase<string>> entities)
        {
            throw new NotImplementedException();
        }

        OperationResult IUserManagerDataService.UpdateEntity(DTOEntityBase<string> entity)
        {
            throw new NotImplementedException();
        }

        OperationResult IUserManagerDataService.UpdateEntities(List<DTOEntityBase<string>> entities)
        {
            throw new NotImplementedException();
        }

        OperationResult IUserManagerDataService.DeleteEntity(DTOEntityBase<string> entity, bool bRemoveRecord)
        {
            throw new NotImplementedException();
        }

        OperationResult IUserManagerDataService.DeleteEntities(List<DTOEntityBase<string>> entities, bool bRemoveRecord)
        {
            throw new NotImplementedException();
        }
        #endregion Generic
    }
}
