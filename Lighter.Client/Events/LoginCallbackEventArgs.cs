using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace Lighter.Client.Events
{
    internal enum LoginOperationKinds { Login, Logout };

    internal class LoginCallbackEventArgs
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
