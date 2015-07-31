using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Client.Module.UserManager.Interface.Services;
using Client.Module.UserManager.Models;
using Client.ModuleBase.Services;
using Lighter.Client.Infrastructure.Interface;
using Lighter.ModuleServiceBase.Model;
using Lighter.UserManagerService.Interface;
using Lighter.UserManagerService.Model;
using Microsoft.Practices.ServiceLocation;

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
