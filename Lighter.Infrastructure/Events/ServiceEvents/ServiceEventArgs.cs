using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Utility;

namespace Lighter.Client.Infrastructure.Events.ServiceEvents
{
    public enum ServiceEventKind
    {
        /// <summary>
        /// 没有找到服务端
        /// </summary>
        [Description("没有找到服务端")]
        NotFound,
        /// <summary>
        /// 服务端太忙
        /// </summary>
        [Description("服务端太忙")]
        TooBusy,
        /// <summary>
        /// 服务端已关闭
        /// </summary>
        [Description("服务端已关闭")]
        Closed
    }

    public class ServiceEventArgs : SimpleMessageEventArgs
    {
        public ServiceEventKind Kind { get; private set; }

        public ServiceEventArgs(ServiceEventKind kind, string message = null)
            : base(message == null ? kind.ToDescription() : message)
        {
            Kind = kind;
        }
    }
}
