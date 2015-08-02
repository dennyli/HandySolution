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
using System;
using Utility;
using System.ServiceModel;
using System.ServiceModel.Channels;

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
        public UserManagerDataService(IServiceLocator serviceLocator, ILighterClientContext lighterContext)
            : base(serviceLocator, lighterContext)
        {
            
        }

        public Accounts GetAccounts()
        {
            ILighterUserManagerService service = GetServerService(typeof(UserManagerModuleInit)) as ILighterUserManagerService;
            Debug.Assert(service != null);

            try
            {
                using (OperationContextScope scope = new OperationContextScope(service as IContextChannel))
                {
                    MessageHeader header = MessageHeader.CreateHeader("userName", "http://www.codestar.com", _lighterContext.GetCurrentAccountName());
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);

                    //List<DTOEntityBase<string>> dtos = service.GetDTOEntities(typeof(AccountDTO));
                    Accounts accounts = new Accounts();
                    //foreach (DTOEntityBase<string> dto in dtos)
                    //    accounts.Add(dto as AccountDTO);

                    List<AccountDTO> dtos = service.GetAccounts();
                    foreach (AccountDTO dto in dtos)
                        accounts.Add(dto);

                    return accounts;
                }
            }
            catch (ProtocolException ex)
            {
                Debug.WriteLine(CommonUtility.GetErrorMessageFromException(ex));
                // Todo
            }
            catch (Exception ex)
            {
                Debug.WriteLine(CommonUtility.GetErrorMessageFromException(ex));
            }

            return null;
        }
    }
}
