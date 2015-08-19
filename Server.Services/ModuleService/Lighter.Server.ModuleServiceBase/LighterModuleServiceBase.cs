using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using Lighter.BaseService;
using Lighter.Data;
using Lighter.ModuleServiceBase.Interface;
using Lighter.ModuleServiceBase.Model;
using Lighter.Server.Common;
using Utility;
using Lighter.ModuleServiceBase.DtoMapping;

namespace Lighter.ModuleServiceBase
{
    public abstract class LighterModuleServiceBase : LighterServiceBase, ILighterModuleService
    {
        [Import]
        protected ILighterServerContext _lighterServerContext { get; set; }

        /// <summary>
        /// 服务的唯一标识
        /// </summary>
        /// <returns>服务标识</returns>
        public abstract string GetServiceId();
        /// <summary>
        /// 服务对客户端提供的功能模块定义, 功能模块和用户角色和权限相关
        /// </summary>
        /// <returns>功能模块定义列表</returns>
        public abstract List<ModuleDTO> GetSupportedModules();

        private bool bInitialized = false;

        /// <summary>
        /// 检查服务是否已初始化，未初始化，可能导致数据转换不正确
        /// </summary>
        public bool IsInitialized
        {
            get { return bInitialized; }
            set { bInitialized = value; }
        }

        /// <summary>
        /// 服务初始化，服务端调用，不开放给客户端
        /// 子类重载此接口后，必须首先调用Base.Initialize, 方便初始化内部对象信息，否者可能导致不正确
        /// </summary>
        public override void Initialize()
        {
            bInitialized = true;
        }

        #region IDisposable 成员

        public override void Dispose()
        {
        }

        #endregion
    }
}
