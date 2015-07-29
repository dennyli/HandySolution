using System;
using System.ComponentModel.Composition;
using Client.Module.UserManager.Interface;
using Client.Module.UserManager.Models;
using Client.ModuleBase.Services;
using Microsoft.Practices.ServiceLocation;
using Lighter.Client.Infrastructure.Interface;
using System.Collections.ObjectModel;
using Lighter.UserManagerService.Model;
using Lighter.UserManagerService.Interface;
using System.Diagnostics;
using System.Collections.Generic;
using Lighter.ModuleServiceBase.Model;

namespace Client.Module.UserManager.Services
{
    [Export(typeof(IDepartmentManagerDataService))]
    public class DepartmentManagerDataService : DataServiceBase, IDepartmentManagerDataService
    {
        [ImportingConstructor]
        public DepartmentManagerDataService(IServiceLocator serviceLocator, ILighterContext lighterContext)
            : base(serviceLocator, lighterContext)
        {
            
        }

        public Departments GetDepartments()
        {
            ILighterUserManagerService service = GetServerService(typeof(UserManagerModuleInit)) as ILighterUserManagerService;
            Debug.Assert(service != null);

            List<DTOEntityBase<string>> dtos = service.GetDTOEntities(typeof(DepartmentDTO));

            Departments departments = new Departments();
            foreach (DTOEntityBase<string> dto in dtos)
                departments.Add(dto as DepartmentDTO);

            return departments;
        }
    }
}
