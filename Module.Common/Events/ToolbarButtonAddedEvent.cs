
using Microsoft.Practices.Prism.Events;
using System.Collections.ObjectModel;
using Module.Common.Commands;

namespace Module.Common.Events
{
    public class ToolbarButtonAddedEvent : CompositePresentationEvent<ObservableCollection<CommandInfo>>
    {
    }
}
