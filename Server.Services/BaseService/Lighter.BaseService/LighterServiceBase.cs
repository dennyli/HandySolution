using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Logging;
using Utility;
using System.ServiceModel;
using Lighter.Server.Common;
using Lighter.BaseService.Interface;

namespace Lighter.BaseService
{
    public abstract class LighterServiceBase : /*CoreServiceBase,*/ ILighterService, IDisposable
    {
        [Import]
        public ILoggerFacade Logger { get; set; }

        public abstract void Initialize();

        #region IDisposable 成员

        public abstract void Dispose();

        #endregion
    }
}
