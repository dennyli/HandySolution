using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Lighter.ServiceManager.TokenValidation
{
    internal class TokenValidationServiceBehavior : IServiceBehavior 
    {
        #region Fields
        private readonly CompositionContainer _container;
        #endregion

        public TokenValidationServiceBehavior(CompositionContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
        }

        #region IServiceBehavior 成员

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                if (dispatcher != null)
                {
                    foreach (var endpointDispatcher in dispatcher.Endpoints)
                    {
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new TokenValidationInspector(_container));
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}
