using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using System.Windows.Input;

namespace Client.Module.Common.Events
{
    public class RoutedUICommandSelectedEvent : CompositePresentationEvent<ICommand>
    {
    }
}
