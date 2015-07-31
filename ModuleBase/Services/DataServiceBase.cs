using System;
using Client.Module.Common.Interface;
using Lighter.Client.Infrastructure.Interface;
using Microsoft.Practices.ServiceLocation;
using Lighter.BaseService.Interface;
using System.Diagnostics;
using Microsoft.Practices.Prism.Modularity;
using System.Collections.Generic;

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
            try
            {
                //IModuleInit moduleInit = _serviceLocator.GetInstance(moduleInitType, moduleInitType.FullName) as IModuleInit;
                
                IEnumerable<IModule> ms = _serviceLocator.GetAllInstances<IModule>();
                IModuleInit moduleInit = null;
                foreach (IModule m in ms)
                {
                    if (m.GetType() == moduleInitType)
                    {
                        moduleInit = m as IModuleInit;
                        break;
                    }
                }
                if (moduleInit == null)
                    return null;

                IModuleResources resource = moduleInit.GetModuleResources();

                return _lighterContext.FindService(resource.GetServiceName());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
