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

        /// <summary>
        /// 服务的唯一标识
        /// </summary>
        /// <returns>服务标识</returns>
        public virtual string GetServiceId() { return string.Empty; }

        /// <summary>
        /// 服务对客户端提供的功能模块定义, 功能模块和用户角色和权限相关
        /// </summary>
        /// <returns>功能模块定义列表</returns>
        public virtual IEnumerable<ModuleDefination> GetModules() { return null; }

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
