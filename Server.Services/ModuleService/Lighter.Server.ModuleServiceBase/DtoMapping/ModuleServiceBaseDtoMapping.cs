using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using AutoMapper;
using Lighter.Data.Dto2Entity;
using Lighter.Data;
using Lighter.ModuleServiceBase.Model;
using AutoMapper.Impl;

namespace Lighter.ModuleServiceBase.DtoMapping
{
    [Export(typeof(IModuleServiceBaseDtoMapping))]
    public class ModuleServiceBaseDtoMapping : DtoMappingBase, IModuleServiceBaseDtoMapping
    {
        #region DtoMappingBase abstract 成员

        public override void InitializeMapping<TKey>()
        {
            // Entity to DTO
            TypeMap tm = Mapper.FindTypeMapFor<EntityBase<string>, DTOEntityBase<string>>();
            if (tm != null)
            {
                if (!tm.IncludedDerivedTypes.Contains<TypePair>(new TypePair(typeof(Module), typeof(ModuleDTO))))
                    tm.IncludeDerivedTypes(typeof(Module), typeof(ModuleDTO));
            }
            else
            {
                Mapper.CreateMap<EntityBase<string>, DTOEntityBase<string>>()
                    .Include<Module, ModuleDTO>();
            }

            tm = Mapper.FindTypeMapFor<Module, ModuleDTO>();
            if (tm == null)
                Mapper.CreateMap<Module, ModuleDTO>();

            // DTO to Entity
            tm = Mapper.FindTypeMapFor<DTOEntityBase<string>, EntityBase<string>>();
            if (tm != null)
            {
                if (!tm.IncludedDerivedTypes.Contains<TypePair>(new TypePair(typeof(ModuleDTO), typeof(Module))))
                    tm.IncludeDerivedTypes(typeof(ModuleDTO), typeof(Module));
            }
            else
            {
                Mapper.CreateMap<DTOEntityBase<string>, EntityBase<string>>()
                    .Include<ModuleDTO, Module>()
                    .ForMember(entity => entity.LastDate, opt => opt.Ignore());
            }

            tm = Mapper.FindTypeMapFor<ModuleDTO, Module>();
            if (tm == null)
                Mapper.CreateMap<ModuleDTO, Module>();

            // 子类的DTO Mapping初始化
            DtoMappingInitialize();

            // 验证配置
            Mapper.AssertConfigurationIsValid();
        }

        public override bool CheckIdIsValid<TKey>(TKey key)
        {
            return key != null;
        }

        #endregion

        #region IModuleServiceBaseDtoMapping 成员

        public virtual void DtoMappingInitialize() { }

        #endregion
    }
}
