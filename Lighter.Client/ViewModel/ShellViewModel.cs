using System.ComponentModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Lighter.Client.Infrastructure.Interface;

namespace Lighter.Client.ViewModel
{
    [Export]
    public class ShellViewModel : INotifyPropertyChanged
    {
        public ShellViewModel()
        {
            // Initialize this ViewModel's commands.
            ExitCommand = new DelegateCommand<object>(AppExit, CanAppExit);
        }

        [Import]
        public ILighterClientContext _lighterContext { get; set; }
        public string Title { get { return _lighterContext.GetClientName(); } }

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
