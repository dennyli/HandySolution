using System;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Practices.Prism.Logging;

namespace Lighter.ServiceManager.ErrorHandler
{
    [Export(typeof(ILighterErrorHandler))]
    public class LighterErrorHandler : ILighterErrorHandler
    {
        [Import]
        public ILoggerFacade Logger { get; set; }

        #region IErrorHandler 成员

        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var newEx = new FaultException(string.Format("服务端错误 {0}", error.TargetSite.Name));
            MessageFault msgFault = newEx.CreateMessageFault();

            fault = Message.CreateMessage(version, msgFault, newEx.Action);
        }

        #endregion
    }
}
