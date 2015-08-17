﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using Lighter.ModuleServiceBase;
using Lighter.ModuleServiceBase.Model;
using Lighter.ServiceManager;
using Lighter.ServiceManager.Endpoints;
using Lighter.UserManagerService.Defination;
using Lighter.UserManagerService.Interface;
using Lighter.UserManagerService.Model;
using Lighter.UserManagerService.UserManagerData;
using Utility;
using Lighter.Data.Dto2Entity;

namespace Lighter.UserManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, Namespace = "http://www.codestar.com/")]
    [ExportService("LighterUserManagerService", "用户管理服务", typeof(LighterUserManagerService), typeof(ILighterUserManagerService), 1), TcpEndpoint(40002)]
    //[ServiceErrorHandlerBehavior(typeof(ServiceErrorHandler))]

    [ServiceKnownType(typeof(AccountDTO))]
    [ServiceKnownType(typeof(DepartmentDTO))]
    [ServiceKnownType(typeof(RoleDTO))]
    [ServiceKnownType(typeof(ModuleDTO))]
    [ServiceKnownType(typeof(DTOEntityBase<string>))]
    public class LighterUserManagerService : LighterModuleServiceBase, ILighterUserManagerService, IDisposable
    {
        [Import]
        protected IUserManagerDataService _dataService;

        public override string GetServiceId()
        {
            return UserManagerDefination.ServiceId;
        }

        public override List<ModuleDTO> GetSupportedModules()
        {
            return _dataService.GetSupportedModules();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        #region  Explict Declare
        public List<AccountDTO> GetAccounts()
        {
            return _dataService.GetAccounts();
        }

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
        #endregion  Explict Declare

        #region Generic
        public DTOEntityBase<string> GetDTOEntity(string key, Type type)
        {
            return _dataService.GetDTOEntity(key, type);
        }

        public List<DTOEntityBase<string>> GetDTOEntities(Type type)
        {
            try
            {
                return _dataService.GetDTOEntities(type);
            }
            catch (Exception ex)
            {
                throw new FaultException<Exception>(ex);
            }
        }

        public OperationResult AddEntity(DTOEntityBase<string> entity)
        {
            return _dataService.AddEntity(entity);
        }
        public OperationResult AddEntities(List<DTOEntityBase<string>> entities)
        {
            return _dataService.AddEntities(entities);
        }

        public OperationResult UpdateEntity(DTOEntityBase<string> entity)
        {
            return _dataService.UpdateEntity(entity);
        }

        public OperationResult UpdateEntities(List<DTOEntityBase<string>> entities)
        {
            return _dataService.UpdateEntities(entities);
        }

        public OperationResult DeleteEntity(DTOEntityBase<string> entity, bool bRemoveRecord)
        {
            return _dataService.DeleteEntity(entity, bRemoveRecord);
        }

        public OperationResult DeleteEntities(List<DTOEntityBase<string>> entities, bool bRemoveRecord)
        {
            return _dataService.DeleteEntities(entities, bRemoveRecord);
        }
        #endregion 
    
        #region IDisposable 成员

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
