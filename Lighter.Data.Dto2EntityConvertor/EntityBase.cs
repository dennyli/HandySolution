// 源文件头信息：
// <copyright file="Entity.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：Utility
// 最后修改：郭明锋
// 最后修改：2013/05/14 23:04
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Lighter.Data.Dto2Entity
{
    /// <summary>
    ///     可持久到数据库的领域模型的基类。
    /// </summary>
    [DataContract]
    public abstract class EntityBase<TKey>
    {
        #region 构造函数

        /// <summary>
        ///     数据实体基类
        /// </summary>
        protected EntityBase()
        {
            IsDeleted = false;
            LastDate = DateTime.Now;
        }

        #endregion

        #region 属性

        [Key]
        [DataMember]
        public TKey Id { get; set; }

        /// <summary>
        ///     获取或设置 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [DataMember]
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     获取或设置 添加时间
        /// </summary>
        [DataType(DataType.DateTime)]
        [DataMember]
        public Nullable<DateTime> LastDate { get; set; }

        #endregion

        public override string ToString()
        {
            return "Id:" + Id.ToString()
                + "\nLastDate:" + LastDate.ToString()
                + "\nIsDeleted:" + IsDeleted.ToString();
        }
    }
}