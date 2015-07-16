using System;
using Client.Module.Common.Interface;

namespace Client.ModuleBase.Services
{
    /// <summary>
    /// Dummy data service class. Provides a dummy data model.
    /// Replace with your real data model and/or data service proxy.
    /// </summary>
    //[Export(typeof(IDataService))] // Export the DataService concrete type. By default, this will be a singleton (shared).
    public abstract class DataServiceBase : IDataService
    {
        public abstract Object GetModel();
    }
}
