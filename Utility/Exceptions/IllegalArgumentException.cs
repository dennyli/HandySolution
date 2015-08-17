using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Utility.Exceptions
{
    public class IllegalArgumentException : ArgumentException
    {
        /// <summary>
        ///     实体化一个 Utility.DalException 类的新实例
        /// </summary>
        public IllegalArgumentException(string argName, Type type)
            : base("参数类型不匹配: " + argName + "期望类型 " + type.ToString())
        { }

        /// <summary>
        ///     使用异常消息实例化一个 Utility.DalException 类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public IllegalArgumentException(string message)
            : base(message) { }

        /// <summary>
        ///     使用异常消息与一个内部异常实例化一个 Utility.DalException 类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在DalException内部的异常实例</param>
        public IllegalArgumentException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        ///     使用可序列化数据实例化一个 Utility.DalException 类的新实例
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        protected IllegalArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
