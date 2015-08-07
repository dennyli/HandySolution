using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Client.Infrastructure.Events
{
    /// <summary>
    /// 基本的消息事件参数
    /// </summary>
    public class SimpleMessageEventArgs : EventArgs
    {
        /// <summary>
        /// 基本的消息
        /// </summary>
        public string Message { get; private set; }

        public SimpleMessageEventArgs(string message = null)
        {
            Message = message;
        }
    }
}
