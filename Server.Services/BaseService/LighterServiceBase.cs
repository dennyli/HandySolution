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
    public abstract class LighterServiceBase : CoreServiceBase, ILighterService
    {
        [Import]
        public ILoggerFacade Logger { get; set; }
    }
}
