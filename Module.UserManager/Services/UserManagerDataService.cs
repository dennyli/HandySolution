using System;
using System.ComponentModel.Composition;
using Client.Module.Common.Interface;
using Client.ModuleBase.Services;
using Client.Module.UserManager.Interface;
using Lighter.Client.Infrastructure.Interface;
using Lighter.UserManagerService.Model;
using System.Collections.ObjectModel;
using Lighter.UserManagerService.Interface;
using Lighter.ModuleServiceBase.Model;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.ServiceLocation;
using Client.Module.UserManager.Models;

namespace Client.Module.UserManager.Services
{
    /// <summary>
    /// Dummy data service class. Provides a dummy data model.
    /// Replace with your real data model and/or data service proxy.
    /// </summary>
    [Export(typeof(IUserManagerDataService))] // Export the DataService concrete type. By default, this will be a singleton (shared).
    public class UserManagerDataService : DataServiceBase, IUserManagerDataService
    {
        
        [ImportingConstructor]
        public UserManagerDataService(IServiceLocator serviceLocator, ILighterContext lighterContext)
            : base(serviceLocator, lighterContext)
        {
            
        }

        public Accounts GetAccounts()
        {
            ILighterUserManagerService service = GetServerService(typeof(UserManagerModuleInit)) as ILighterUserManagerService;
            Debug.Assert(service != null);

            List<DTOEntityBase<string>> dtos = service.GetDTOEntities(typeof(AccountDTO));

            Accounts accounts = new Accounts();
            foreach (DTOEntityBase<string> dto in dtos)
                accounts.Add(dto as AccountDTO);

            return accounts;
        }

        //public ObservableCollection<DepartmentDTO> GetDepartments()
        //{
        //    throw new NotImplementedException();
        //}

        //public ObservableCollection<RoleDTO> GetRoles()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
