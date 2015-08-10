using System;
using Client.Module.Common.Interface;
using Lighter.Client.Infrastructure.Interface;
using Microsoft.Practices.ServiceLocation;
using Lighter.BaseService.Interface;
using System.Diagnostics;
using Microsoft.Practices.Prism.Modularity;
using System.Collections.Generic;
using Lighter.MainService.Interface;
using System.ServiceModel;
using Utility.Exceptions;

namespace Client.ModuleBase.Services
{
    /// <summary>
    /// Dummy data service class. Provides a dummy data model.
    /// Replace with your real data model and/or data service proxy.
    /// </summary>
    //[Export(typeof(IDataService))] // Export the DataService concrete type. By default, this will be a singleton (shared).
    public abstract class DataServiceBase : IDataService
    {
        protected readonly ILighterClientContext _lighterContext;
        protected readonly IServiceLocator _serviceLocator;

        public DataServiceBase(IServiceLocator serviceLocator, ILighterClientContext lighterContext)
        {
            _lighterContext = lighterContext;
            _serviceLocator = serviceLocator;
        }

        public virtual ILighterMainService GetMainService()
        {
            if (_lighterContext != null)
                return _lighterContext.GetMainService();

            return null;
        }

        public virtual ILighterService GetServerService(string serviceKey)
        {
            try
            {
                return _lighterContext.FindService(serviceKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public virtual T InitilizeServerService<T>(string serviceKey, InstanceContext contextCallback = null)
        {
            try
            {
                return _lighterContext.CreateServiceByMainService<T>(serviceKey, contextCallback);
            }
            catch (ServerNotFoundException ex)
            {
                throw ex;
            }
            catch (ServerTooBusyException ex)
            {
                throw ex;
            }
        }
    }
}
