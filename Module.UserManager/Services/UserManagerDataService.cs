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
using Utility.Exceptions;

namespace Client.Module.UserManager.Services
{
    /// <summary>
    /// Dummy data service class. Provides a dummy data model.
    /// Replace with your real data model and/or data service proxy.
    /// </summary>
    [Export(typeof(IUserManagerDataService))] // Export the DataService concrete type. By default, this will be a singleton (shared).
    public class UserManagerDataService : UMDataService, IUserManagerDataService
    {
        
        [ImportingConstructor]
        public UserManagerDataService(IServiceLocator serviceLocator, ILighterClientContext lighterContext)
            : base(serviceLocator, lighterContext)
        {
            
        }

    }
}
