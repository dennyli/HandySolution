using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Lighter.Client.Infrastructure.Accounts;

namespace Lighter.Client.Infrastructure.TokenValidation
{
    public class TokenValidationBehavior : IEndpointBehavior
    {
        private Account _account = null;
        public TokenValidationBehavior(Account account)
        {
            _account = account;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new TokenValiadtionClientMessageInspector(_account));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
