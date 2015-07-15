using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.ServiceHostManager.Hosting;

namespace Lighter.ServiceHostManager
{
    /// <summary>
    /// Defines the required contract for implementing a Service Host Manager.
    /// </summary>
    public interface IServiceHostManager
    {
        #region Properties
        /// <summary>
        /// Gets the set of service hosts.
        /// </summary>
        IEnumerable<ExportServiceHost> Services { get; }
        #endregion
    }
}
