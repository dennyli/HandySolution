using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Client.Module.Common.Interface
{
    /// <summary>
    /// Data service interface.
    /// </summary>
    public interface IDataService
    {
        Object GetModel();
    }
}
