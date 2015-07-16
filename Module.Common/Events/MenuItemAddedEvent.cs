using Microsoft.Practices.Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Client.Module.Common.Events
{
    public class MenuItemAddedEvent : CompositePresentationEvent<ObservableCollection<MenuItem>>
    {
    }
}
