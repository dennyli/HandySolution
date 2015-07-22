using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;
using Lighter.BaseService.Implement;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Logging;
using Utility;
using System.ServiceModel;
using Lighter.Server.Common;

namespace Lighter.BaseService
{
    public abstract class LighterServiceBase : CoreServiceBase, ILighterService
    {
        [Import]
        public ILoggerFacade Logger { get; set; }

        public virtual OperationResult CheckSession()
        {
            SessionState state = OperationContext.Current.InstanceContext.Extensions.Find<SessionState>();
            if (state == null)
                return new OperationResult(OperationResultType.IllegalOperation);

            SessionState stateChecked = null;
            LighterSessionStateManager manager = LighterServerContext.GetInstance().SessionManager;
            if (!manager.TryGetValue(state.Account, out stateChecked))
                return new OperationResult(OperationResultType.IllegalOperation);

            return new OperationResult(OperationResultType.Success);
        }
    }
}
