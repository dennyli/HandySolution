using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using Lighter.Server.Common;

namespace Lighter.ServiceManager.TokenValidation
{
    internal class TokenValidationInspector : IDispatchMessageInspector
    {
        #region Fields
        private readonly CompositionContainer _container;
        #endregion

        public TokenValidationInspector(CompositionContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
        }

        #region IDispatchMessageInspector 成员

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            int index = OperationContext.Current.IncomingMessageHeaders.FindHeader("userId", "http://www.codestar.com");
            if (index < 0)
                throw new FaultException("操作非法");

            string userId = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index).ToString();
            if (string.IsNullOrWhiteSpace(userId))
                throw new FaultException("操作非法");

            ILighterServerContext serverContext = _container.GetExportedValue<ILighterServerContext>();
            if (!serverContext.IsAccountLogined(userId))
                throw new FaultException("操作非法");

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }

        #endregion
    }
}
