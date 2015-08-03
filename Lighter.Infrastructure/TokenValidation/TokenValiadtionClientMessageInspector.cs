using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Lighter.Client.Infrastructure.Accounts;

namespace Lighter.Client.Infrastructure.TokenValidation
{
    public class TokenValiadtionClientMessageInspector : IClientMessageInspector
    {
        private Account _account = null;
        public TokenValiadtionClientMessageInspector(Account account)
        {
            _account = account;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (_account != null)
            {
                MessageHeader userIdHeader = MessageHeader.CreateHeader("userId", "http://www.codestar.com", _account.Id);
                request.Headers.Add(userIdHeader);
            }

            return null;
        }
    }
}
