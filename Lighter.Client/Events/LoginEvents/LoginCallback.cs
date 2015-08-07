using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;

namespace Lighter.Client.Events.LoginEvents
{
    [Export(typeof(ILoginCallback))]
    public class LoginCallback : ILoginCallback
    {
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public LoginCallback(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void LoginResult(OperationResult or)
        {
            _eventAggregator.GetEvent<LoginCallbackEvent>().Publish(new LoginCallbackEventArgs(LoginOperationKinds.Login, or));
        }

        public void LogoutResult(OperationResult or)
        {
            _eventAggregator.GetEvent<LoginCallbackEvent>().Publish(new LoginCallbackEventArgs(LoginOperationKinds.Logout, or));
        }
    }
}
