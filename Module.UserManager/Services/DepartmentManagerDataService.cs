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
    public class DepartmentManagerDataService : UMDataService, IDepartmentManagerDataService
    {
        [ImportingConstructor]
        public DepartmentManagerDataService(IServiceLocator serviceLocator, ILighterClientContext lighterContext)
            : base(serviceLocator, lighterContext)
        {
            
        }

    }
}
