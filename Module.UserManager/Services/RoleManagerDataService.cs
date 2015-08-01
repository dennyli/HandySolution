using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Module.UserManager.Interface.Services;
using Client.ModuleBase.Services;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using Lighter.Client.Infrastructure.Interface;
using Client.Module.UserManager.Models;
using Lighter.UserManagerService.Interface;
using System.Diagnostics;
using Lighter.UserManagerService.Model;
using Lighter.ModuleServiceBase.Model;

namespace Client.Module.UserManager.Services
{
    [Export(typeof(IRoleManagerDataService))]
    public class RoleManagerDataService : DataServiceBase, IRoleManagerDataService
    {
        [ImportingConstructor]
        public RoleManagerDataService(IServiceLocator serviceLocator, ILighterClientContext lighterContext)
            : base(serviceLocator, lighterContext)
        {
            
        }

        public Roles GetRoles()
        {
            ILighterUserManagerService service = GetServerService(typeof(UserManagerModuleInit)) as ILighterUserManagerService;
            Debug.Assert(service != null);

            List<DTOEntityBase<string>> dtos = service.GetDTOEntities(typeof(RoleDTO));

            Roles roles = new Roles();
            foreach (DTOEntityBase<string> dto in dtos)
                roles.Add(dto as RoleDTO);

            return roles;
        }
    }
}
