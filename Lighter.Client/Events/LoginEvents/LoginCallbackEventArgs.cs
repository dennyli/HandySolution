using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.ComponentModel;

namespace Lighter.Client.Events.LoginEvents
{
    internal enum LoginOperationKinds { 
        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        Login, 
        /// <summary>
        /// 登出
        /// </summary>
        [Description("登出")]
        Logout };

    internal class LoginCallbackEventArgs : EventArgs
    {
        public LoginOperationKinds Kind { get; private set; }
        public OperationResult OpResult { get; private set; }

        public LoginCallbackEventArgs(LoginOperationKinds kind, OperationResult or)
        {
            Kind = kind;
            OpResult = or;
        }
    }
}
