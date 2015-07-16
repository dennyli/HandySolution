using System;
using System.ComponentModel.Composition;
using Client.Module.Common.Interface;
using Client.ModuleBase.Services;
using Client.Module.UserManager.Interface;

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
        public UserManagerDataService()
        {
            
        }

        public override Object GetModel()
        {
            return null;
        }
    }
}
