using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace Lighter.Client.Events.InputEvents
{
    internal class InputErrorEvent : CompositePresentationEvent<InputErrorEventArgs>
    {
    }
}
