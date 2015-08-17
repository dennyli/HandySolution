using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.ModuleBase.Services;
using Microsoft.Practices.ServiceLocation;
using Lighter.Client.Infrastructure.Interface;
using Client.Module.UserManager.Models;
using Lighter.UserManagerService.Interface;
using Lighter.ModuleServiceBase.Model;
using System.Diagnostics;
using Lighter.UserManagerService.Model;
using Utility;
using Utility.Exceptions;
using System.ServiceModel;
using Lighter.Data.Dto2Entity;

namespace Client.Module.UserManager.Services
{
    public class UMDataService : DataServiceBase
    {
        public UMDataService(IServiceLocator serviceLocator, ILighterClientContext lighterContext)
            : base(serviceLocator, lighterContext)
        {
        }

        public Accounts GetAccounts()
        {
            ILighterUserManagerService service = GetServerService(UserManagerResources.SERVICE_NAME) as ILighterUserManagerService;
            Debug.Assert(service != null);

            try
            {
                Accounts accounts = new Accounts();

                List<AccountDTO> dtos = service.GetAccounts();
                foreach (AccountDTO dto in dtos)
                    accounts.Add(dto);

                return accounts;
                //}
            }
            catch (ServerClosedException ex)
            {
                throw ex;
            }
            catch (ProtocolException ex)
            {
                Debug.WriteLine(CommonUtility.GetErrorMessageFromException(ex));

                throw new ServerClosedException();
            }
            catch (CommunicationException ex)
            {
                Debug.WriteLine(CommonUtility.GetErrorMessageFromException(ex));

                throw ex;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(CommonUtility.GetErrorMessageFromException(ex));
            }

            return null;
        }

        public Roles GetRoles()
        {
            ILighterUserManagerService service = GetServerService(UserManagerResources.SERVICE_NAME) as ILighterUserManagerService;
            Debug.Assert(service != null);

            List<DTOEntityBase<string>> dtos = service.GetDTOEntities(typeof(RoleDTO));

            Roles roles = new Roles();
            foreach (DTOEntityBase<string> dto in dtos)
                roles.Add(dto as RoleDTO);

            return roles;
        }

        public Departments GetDepartments()
        {
            ILighterUserManagerService service = GetServerService(UserManagerResources.SERVICE_NAME) as ILighterUserManagerService;
            Debug.Assert(service != null);

            List<DTOEntityBase<string>> dtos = service.GetDTOEntities(typeof(DepartmentDTO));

            Departments departments = new Departments();
            foreach (DTOEntityBase<string> dto in dtos)
                departments.Add(dto as DepartmentDTO);

            return departments;
        }
    }
}
