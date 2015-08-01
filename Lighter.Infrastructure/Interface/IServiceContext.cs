using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;

namespace Lighter.Client.Infrastructure.Interface
{
    public interface IServiceContext
    {
        #region Services
        
        void AddService(string key, ILighterService service);

        ILighterService FindService(string key);

        void RemoveService(string key);

        void CreateMainService();

        #endregion // Services
    }
}
