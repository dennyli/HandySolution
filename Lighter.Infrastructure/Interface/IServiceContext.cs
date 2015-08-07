using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;
using Lighter.MainService.Interface;
using System.ServiceModel;

namespace Lighter.Client.Infrastructure.Interface
{
    public interface IServiceContext
    {
        #region Services
        
        void AddService(string key, ILighterService service);

        ILighterService FindService(string key);

        void RemoveService(string key);

        ILighterMainService GetMainService();

        T CreateServiceByMainService<T>(string serviceName, InstanceContext contextCallback = null, bool bTokenValidation = true);

        #endregion // Services
    }
}
