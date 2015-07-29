using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Lighter.BaseService.Interface;

namespace Client.Module.Common.Interface
{
    /// <summary>
    /// Data service interface.
    /// </summary>
    public interface IDataService
    {
        ILighterService GetServerService(Type moduleInitType);
    }
}
