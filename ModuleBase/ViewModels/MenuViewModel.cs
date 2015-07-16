using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using Client.Module.Common.Events;
using Client.Module.Common.Exceptions;
using Client.ModuleBase.Views;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Client.ModuleBase.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MenuViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceLocator _serviceLocator;
        //private readonly LighterContext _context;

        [ImportingConstructor]
        public MenuViewModel(IEventAggregator eventAggregator, IServiceLocator serviceLocator)
        {
            _eventAggregator = eventAggregator;
            _serviceLocator = serviceLocator;

            //_context = LighterContext.GetInstance();
            MenuItems = new ObservableCollection<MenuItem>();

            _eventAggregator.GetEvent<MenuItemAddedEvent>().Subscribe(MenuItemAdded);
        }

        private void MenuItemAdded(ObservableCollection<MenuItem> menuItems)
        {
            if ((menuItems != null) && (menuItems.Count > 0))
            {
                IList<MenuItem> addedCommand = UpdateMenu(MenuItems, menuItems);
                if (addedCommand.Count > 0)
                {
                    MenuView view = _serviceLocator.GetInstance<MenuView>();
                    foreach (MenuItem item in addedCommand)
                        view.CommandBindings.AddRange(item.CommandBindings);

                    this.RaisePropertyChanged("MenuItems");
                }
            }
        }

        /// <summary>
        /// Property for UI Binding
        /// </summary>
        public ObservableCollection<MenuItem> MenuItems { get; private set; }

        #region Menu Combine
        private IList<MenuItem> UpdateMenu(IList<MenuItem> globalItems, IList<MenuItem> selfItems)
        {
            if (globalItems == null)
                return null;

            if (selfItems == null)
                return null;

            IList<MenuItem> addedItems = new List<MenuItem>();

            foreach (MenuItem self in selfItems)
            {
                var results = globalItems.Where<MenuItem>(item => item.HasHeader && self.HasHeader && (self.Header == item.Header));
                int count = results.Count<MenuItem>();
                if (count == 0)
                {
                    globalItems.Add(self);
                    AddSelfCommand2List(addedItems, self);
                }

                else if (count > 1)
                    throw new MenuItemConflictException("Menu item: " + self.Header);
                else
                {
                    MenuItem item = results.First<MenuItem>();
                    if (self.HasItems)
                    {
                        if (item.HasItems)
                            UpdateMenu(item.Items, self.Items, addedItems);
                        else
                        {
                            foreach (MenuItem sub in self.Items)
                            {
                                item.Items.Add(sub);
                                if (sub.Command != null)
                                    addedItems.Add(sub);
                            }
                        }
                    }

                }
            }

            return addedItems;
        }

        private void UpdateMenu(ItemCollection globalItems, ItemCollection selfItems, IList<MenuItem> addedItems)
        {
            if (globalItems == null)
                return;

            if (selfItems == null)
                return;

            foreach (MenuItem self in selfItems)
            {
                var results = globalItems.Cast<MenuItem>().Where<MenuItem>(item => item.HasHeader && self.HasHeader && (self.Header == item.Header));
                int count = results.Count<MenuItem>();
                if (count == 0)
                {
                    globalItems.Add(self);
                    AddSelfCommand2List(addedItems, self);
                }
                else if (count > 1)
                    throw new MenuItemConflictException("Menu item: " + self.Header);
                else
                {
                    MenuItem item = results.First<MenuItem>();
                    if (self.HasItems)
                    {
                        if (self.HasItems)
                            UpdateMenu(item.Items, self.Items, addedItems);
                        else if (!item.HasItems)
                        {
                            foreach (MenuItem sub in self.Items)
                            {
                                item.Items.Add(sub);
                                if (sub.Command != null)
                                    addedItems.Add(sub);
                            }
                        }
                    }

                }
            }
        }

        private void AddSelfCommand2List(IList<MenuItem> addedItems, MenuItem item)
        {
            if (item.Command != null)
                addedItems.Add(item);

            AddSelfCommand2List(addedItems, item.Items);
        }

        private void AddSelfCommand2List(IList<MenuItem> addedItems, ItemCollection itemCollection)
        {
            if (itemCollection.IsEmpty)
                return;

            foreach (MenuItem item in itemCollection)
            {
                if (item.Command != null)
                    addedItems.Add(item);

                AddSelfCommand2List(addedItems, item.Items);
            }
        }
        #endregion Menu Combine
    }
}
