using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace Lighter.Client.Infrastructure.Events.ServiceEvents
{
    public class ServiceEvent : CompositePresentationEvent<ServiceEventArgs>
    {
    }
}
