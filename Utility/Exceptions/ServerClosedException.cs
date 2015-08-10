using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Utility.Exceptions
{
    /// <summary>
    /// 服务端关闭
    /// </summary>
    [Serializable]
    public class ServerClosedException : Exception
    {
        public ServerClosedException()
            : base("服务端已关闭") { }

        public ServerClosedException(Exception inner)
            : base("服务端已关闭", inner) { }

        public ServerClosedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
