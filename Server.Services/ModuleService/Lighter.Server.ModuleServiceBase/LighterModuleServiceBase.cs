using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService;
using System.ComponentModel.Composition;
using Lighter.Data;
using Utility;
using Lighter.Data.Repositories;
using Lighter.ModuleServiceBase.Data;
using AutoMapper;

namespace Lighter.ModuleServiceBase
{
    public abstract class LighterModuleServiceBase : LighterServiceBase, ILighterModuleService
    {
        /// <summary>
        /// 服务的唯一标识
        /// </summary>
        /// <returns>服务标识</returns>
        public abstract string GetServiceId();
        /// <summary>
        /// 服务对客户端提供的功能模块定义, 功能模块和用户角色和权限相关
        /// </summary>
        /// <returns>功能模块定义列表</returns>
        public abstract IEnumerable<ModuleDTO> GetModules();

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
        public virtual void Initialize()
        {
            TypeMap baseMap = Mapper.FindTypeMapFor<DTOEntityBase<string>, EntityBase<string>>();
            if (baseMap == null)
                Mapper.CreateMap<DTOEntityBase<string>, EntityBase<string>>();

            if (!baseMap.TypeHasBeenIncluded(typeof(ModuleDTO), typeof(Module)))
                baseMap.IncludeDerivedTypes(typeof(ModuleDTO), typeof(Module));

            if (null == Mapper.FindTypeMapFor<ModuleDTO, Module>())
                Mapper.CreateMap<ModuleDTO, Module>();
        }
    }
}
