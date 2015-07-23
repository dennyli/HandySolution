using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService;
using System.ComponentModel.Composition;
using Lighter.Data;
using Utility;

namespace Lighter.Server.ModuleServiceBase
{
    public abstract class LighterModuleServiceBase : LighterServiceBase, ILighterModuleService
    {
        [Import]
        protected IAccountRepository AccountRepository { get; set; }

        public IQueryable<Account> Accounts
        {
            get { return AccountRepository.Entities; }
        }

        /// <summary>
        /// 服务的唯一标识
        /// </summary>
        /// <returns>服务标识</returns>
        public abstract string GetServiceId();
        /// <summary>
        /// 服务对客户端提供的功能模块定义, 功能模块和用户角色和权限相关
        /// </summary>
        /// <returns>功能模块定义列表</returns>
        public abstract IEnumerable<ModuleDefination> GetModules();
    }
}
