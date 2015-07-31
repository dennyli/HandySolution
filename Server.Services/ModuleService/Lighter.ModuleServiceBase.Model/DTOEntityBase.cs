using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Lighter.ModuleServiceBase.Model
{
    [DataContract]
    public abstract class DTOEntityBase<TKey>
    {
        public DTOEntityBase(Type DTOType)
        {
            this.DTOType = DTOType;
        }

        /// <summary>
        /// 唯一标示，可能是编号，可能是序号，可能是GUID
        /// </summary>
        [Key]
        [DataMember]
        public TKey Id { get; set; }

        /// <summary>
        /// DTO 对象的类型， 简化接口
        /// </summary>
        public Type DTOType { get; private set; }
    }
}
