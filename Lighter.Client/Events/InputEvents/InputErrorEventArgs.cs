using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Utility;
using Lighter.Client.Infrastructure.Events;

namespace Lighter.Client.Events.InputEvents
{
    internal enum InputErrorKinds { 
        /// <summary>
        /// 账户输入不合法
        /// </summary>
        [Description("账户输入不合法, 账户不能为空，长度不能超过16")]
        Account, 
        /// <summary>
        /// 口令输入不合法
        /// </summary>
        [Description("口令输入不合法, 口令不能为空，长度不能超过16")]
        Password };

    internal class InputErrorEventArgs : SimpleMessageEventArgs
    {
        public InputErrorKinds Kind { get; private set; }

        public InputErrorEventArgs(InputErrorKinds kind, string message = null)
            : base(message == null ? kind.ToDescription() : message)
        {
            Kind = kind;
        }
    }
}
