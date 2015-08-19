using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Reflection;
using Lighter.Data.Dto2Entity;
using AutoMapper.Impl;

namespace Lighter.ModuleServiceBase.DtoMapping
{
    public abstract class DtoMappingBase : IDtoMapping
    {
        #region IDtoMapping 成员

        /// <summary>
        /// InitMapping初始化AutoMapper, 在子类中必须重载
        /// </summary>
        public abstract void InitializeMapping<TKey>();

        /// <summary>
        /// 子类中必须重载ID验证是否有效
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public abstract bool CheckIdIsValid<TKey>(TKey src);

        /// <summary>
        /// 加载实体对象。
        /// <remarks></remarks> 
        /// </summary>
        public void LoadEntity<TSource, TMember, TKey>(IMemberConfigurationExpression<TSource> opt,
            Func<TSource, TKey> getId, Func<TKey, TMember> doLoad) where TMember : class
        {
            opt.Condition(src => (getId(src) != null));
            opt.MapFrom(src => CheckIdIsValid<TKey>(getId(src)) ? null : doLoad(getId(src)));
        }

        //public void IgnoreDtoIdAndVersionPropertyToEntity<TKey>()
        //{
            //PropertyInfo idProperty = typeof(EntityBase<TKey>).GetProperty("Id");
            //PropertyInfo versionProperty = typeof(EntityBase<T>).GetProperty("Version");
            //foreach (TypeMap map in Mapper.GetAllTypeMaps())
            //{
            //    if (typeof(DTOEntityBase<TKey>).IsAssignableFrom(map.SourceType)
            //        && typeof(EntityBase<TKey>).IsAssignableFrom(map.DestinationType))
            //    {
            //        map.FindOrCreatePropertyMapFor(new PropertyAccessor(idProperty)).Ignore();
            //        map.FindOrCreatePropertyMapFor(new PropertyAccessor(versionProperty)).Ignore();
            //    }
            //}
        //}

        #endregion
    }
}
