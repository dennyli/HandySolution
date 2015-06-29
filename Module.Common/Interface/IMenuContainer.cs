using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Controls;

namespace Module.Common
{
    public interface IMenuContainer
    {
        IList<MenuItem> GetMenu();

        void UpdateMenu(IList<MenuItem> menuItems);

        IList<MenuItem> MenuItems { get; set; }
    }
}
