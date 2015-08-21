using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using AutoMapper;

namespace Lighter.Data.Dto2Entity
{
    [DataContract]
    public abstract class DTOEntityBase<TKey>
    {
        public DTOEntityBase()
        {
        }

        /// <summary>
        /// 唯一标示，可能是编号，可能是序号，可能是GUID
        /// </summary>
        [Key]
        [DataMember]
        public TKey Id { get; set; }

        /// <summary>
        ///     获取或设置 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id: " + Id.ToString());
            sb.Append("\nType: " + this.GetType().ToString());

            return sb.ToString();
        }

        /// <summary>
        /// 从实体中取得属性值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public virtual void FetchValuesFromEntity<TEntity>(TEntity entity)
        {
            Mapper.Map(entity, this, entity.GetType(), this.GetType());
        }

        /// <summary>
        /// 将DTO中的属性值赋值到实体对象中
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public virtual void AssignValuesToEntity<TEntity>(TEntity entity)
        {
            Mapper.Map(this, entity, this.GetType(), entity.GetType());
        }
    }
}
