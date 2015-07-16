using System.ComponentModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;

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
