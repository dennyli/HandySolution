using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Lighter.ServiceManager.ErrorHandler
{
    public class ServiceErrorHandlerBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly Type _errorHandlerType;

        public ServiceErrorHandlerBehaviorAttribute(Type errorHandlerType)
        {
            _errorHandlerType = errorHandlerType;
        }

        #region IServiceBehavior 成员

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            var handler = (IErrorHandler)Activator.CreateInstance(_errorHandlerType);

            foreach (ChannelDispatcherBase dispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = dispatcherBase as ChannelDispatcher;
                if (channelDispatcher != null)
                    channelDispatcher.ErrorHandlers.Add(handler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}
