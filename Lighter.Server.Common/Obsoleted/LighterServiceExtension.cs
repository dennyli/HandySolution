using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Utility;

namespace Lighter.Server.Common
{
    public class LighterServiceExtension : IInstanceContextProvider, IDispatchMessageInspector
    {
        internal object ThisLock
        {
            get { return this; }
        }

        InstanceContext IInstanceContextProvider.GetExistingInstanceContext(Message message, IContextChannel channel)
        {
            SessionState state = null;

            bool hasSession = (channel.SessionId != null);
            if (hasSession)
            {
                state = channel.Extensions.Find<SessionState>();
                if (state != null)
                    return state.WaitForInstanceContext();
            }

            int headerIndex = message.Headers.FindHeader(ClientHeader.HeaderName, ClientHeader.HeaderNamespace);
            string clientId = null;
            if (headerIndex != -1)
                clientId = message.Headers.GetHeader<string>(headerIndex);

            if (clientId == null)
                return null;

            bool isNew = false;
            lock (this.ThisLock)
            {
                LighterSessionStateManager manager = LighterSessionContext.GetInstance().SessionManager;
                if (!manager.TryGetValue(clientId, out state))
                {
                    state = new SessionState(clientId, null);
                    manager.Add(clientId, state);
                    isNew = true;
                }
            }

            if (hasSession)
                channel.Extensions.Add(state);

            if (isNew)
                return null;

            InstanceContext instanceContext = state.WaitForInstanceContext();
            if (hasSession)
                instanceContext.IncomingChannels.Add(channel);

            return instanceContext;
        }

        void IInstanceContextProvider.InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel)
        {
            SessionState state = null;

            bool hasSession = (channel.SessionId != null);
            if (hasSession)
            {
                instanceContext.IncomingChannels.Add(channel);
                state = channel.Extensions.Find<SessionState>();
            }
            else
            {
                // Otherwise, if we don't have a session, look the info up again in the table.
                int headerIndex = message.Headers.FindHeader(ClientHeader.HeaderName, ClientHeader.HeaderNamespace);
                if (headerIndex != -1)
                {
                    string clientId = message.Headers.GetHeader<string>(headerIndex);
                    if (clientId != null)
                    {
                        lock (this.ThisLock)
                        {
                            LighterSessionStateManager manager = LighterSessionContext.GetInstance().SessionManager;
                            manager.TryGetValue(clientId, out state);
                        }
                    }
                }
            }

            if (state != null)
            {
                instanceContext.Extensions.Add(state);
                state.SetInstanceContext(instanceContext);
            }

            instanceContext.Closing += delegate(object sender, EventArgs e)
            {
                lock (this.ThisLock)
                {
                    LighterSessionStateManager manager = LighterSessionContext.GetInstance().SessionManager;
                    manager.Remove(state.Account);
                }
            };
        }

        bool IInstanceContextProvider.IsIdle(InstanceContext instanceContext)
        {
            return false;
        }

        void IInstanceContextProvider.NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext)
        {
        }

        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return instanceContext;
        }

        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
}
