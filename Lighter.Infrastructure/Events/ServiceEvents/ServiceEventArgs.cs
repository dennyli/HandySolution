using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Client.Infrastructure.Events.ServiceEvents
{
    public enum ServiceEventKind { NotFound, TooBusy, Closed }

    public class ServiceEventArgs : EventArgs
    {
        public ServiceEventKind Kind { get; private set; }
        public string Messsage { get; private set; }

        public ServiceEventArgs(ServiceEventKind kind, string message)
        {
            Kind = kind;
            Messsage = message;
        }
    }
}
