using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;

namespace Lighter.ServiceManager.ErrorHandler
{
    public class EndpointErrorHandlerBehavior : IEndpointBehavior
    {
        #region Fields
        private readonly CompositionContainer _container;
        #endregion

        public EndpointErrorHandlerBehavior(CompositionContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
        }

        #region IEndpointBehavior 成员

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(_container.GetExportedValue<ILighterErrorHandler>());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
