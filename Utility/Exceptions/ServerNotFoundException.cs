using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Utility.Exceptions
{
    /// <summary>
    /// 服务端找不到异常， 可能原因服务端未开启，或者当前用户无权限连接服务器
    /// </summary>
    [Serializable]
    public class ServerNotFoundException : Exception
    {
        public ServerNotFoundException() { }

        public ServerNotFoundException(string message)
            : base(message) { }

        public ServerNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public ServerNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
