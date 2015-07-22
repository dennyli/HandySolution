using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace Lighter.Server.Common
{
    public class SessionState : IExtension<InstanceContext>, IExtension<IContextChannel>
    {
        public SessionState(string account, object data)
        {
            Account = account;
            Data = data;
        }

        public string Account { get; private set; }
        public object Data { get; private set; }

        
        InstanceContext _instanceContext = null;
        ManualResetEvent _waitHandle = null;

        //public InstanceContext InstanceContext
        //{
        //    get { return this._instanceContext; }
        //}

        internal object ThisLock
        {
            get { return this; }
        }

        public void SetInstanceContext(InstanceContext instanceContext)
        {
            lock (this.ThisLock)
            {
                this._instanceContext = instanceContext;
                if (this._waitHandle != null)
                {
                    this._waitHandle.Set();
                }
            }
        }

        public InstanceContext WaitForInstanceContext()
        {
            bool wait = false;

            if (this._instanceContext == null)
            {
                lock (this.ThisLock)
                {
                    if (this._instanceContext == null)
                    {
                        if (this._waitHandle == null)
                        {
                            this._waitHandle = new ManualResetEvent(false);
                        }
                        wait = true;
                    }
                }
            }

            if (wait)
            {
                this._waitHandle.WaitOne();
                this._waitHandle.Close();
            }

            return this._instanceContext;
        }

        // --------------------------------------------------------------------
        // Not used for this scenario
        void IExtension<InstanceContext>.Attach(InstanceContext channel) { }
        void IExtension<InstanceContext>.Detach(InstanceContext channel) { }
        void IExtension<IContextChannel>.Attach(IContextChannel channel) { }
        void IExtension<IContextChannel>.Detach(IContextChannel channel) { }
    }
}
