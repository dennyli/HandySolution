using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Lighter.BaseService.Interface;
using Lighter.MainService.Interface;
using System.ServiceModel;

namespace Client.Module.Common.Interface
{
    /// <summary>
    /// Data service interface.
    /// </summary>
    public interface IDataService
    {
        ILighterMainService GetMainService();

        ILighterService GetServerService(string serviceKey);

        T InitilizeServerService<T>(string serviceKey, InstanceContext contextCallback = null);
    }
}
