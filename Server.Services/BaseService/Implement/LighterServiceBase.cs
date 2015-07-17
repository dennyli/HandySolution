using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;
using Lighter.BaseService.Implement;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Logging;

namespace Lighter.BaseService
{
    public abstract class LighterServiceBase : CoreServiceBase, ILighterService
    {
        [Import]
        public ILoggerFacade Logger { get; set; }
    }
}
