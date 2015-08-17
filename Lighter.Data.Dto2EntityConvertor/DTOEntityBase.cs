using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id: " + Id.ToString());
            sb.Append("\nType: " + this.GetType().ToString());

            return sb.ToString();
        }
    }
}
