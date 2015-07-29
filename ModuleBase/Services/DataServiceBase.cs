using System;
using Client.Module.Common.Interface;
using Lighter.Client.Infrastructure.Interface;
using Microsoft.Practices.ServiceLocation;
using Lighter.BaseService.Interface;

namespace Client.ModuleBase.Services
{
    /// <summary>
    /// Dummy data service class. Provides a dummy data model.
    /// Replace with your real data model and/or data service proxy.
    /// </summary>
    //[Export(typeof(IDataService))] // Export the DataService concrete type. By default, this will be a singleton (shared).
    public abstract class DataServiceBase : IDataService
    {
        protected readonly ILighterContext _lighterContext;
        protected readonly IServiceLocator _serviceLocator;

        public DataServiceBase(IServiceLocator serviceLocator, ILighterContext lighterContext)
        {
            _lighterContext = lighterContext;
            _serviceLocator = serviceLocator;
        }


        public virtual ILighterService GetServerService(Type moduleInitType)
        {
            IModuleInit moduleInit = _serviceLocator.GetInstance(moduleInitType) as IModuleInit;
            IModuleResources resource = moduleInit.GetModuleResources();

            return _lighterContext.FindService(resource.GetServiceName());
        }
    }
}
