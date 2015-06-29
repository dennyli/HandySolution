using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Module.Common;
using System.ComponentModel;

namespace Lighter.Client
{
    [Export]
    public class ShellViewModel: INotifyPropertyChanged
    {
        public ShellViewModel()
        {
            // Initialize this ViewModel's commands.
            ExitCommand = new DelegateCommand<object>(AppExit, CanAppExit);
        }

        #region ExitCommand
        public DelegateCommand<object> ExitCommand { get; private set; }

        private void AppExit(object commandArg)
        {
            App.Current.Shutdown();
        }

        private bool CanAppExit(object commandArg)
        {
            return true;
        }
        #endregion

        

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
