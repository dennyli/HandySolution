
using System.Collections.ObjectModel;
using Client.Module.Common.Commands;
using Microsoft.Practices.Prism.Events;

namespace Client.Module.Common.Events
{
    public class ToolbarButtonAddedEvent : CompositePresentationEvent<ObservableCollection<CommandInfo>>
    {
    }
}
