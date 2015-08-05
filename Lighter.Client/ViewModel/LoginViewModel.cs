using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.Windows;
using Lighter.Client.Events;
using Lighter.Client.Infrastructure.Accounts;
using Lighter.Client.Infrastructure.Interface;
using Lighter.LoginService.Model;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Utility;

namespace Lighter.Client.ViewModel
{
    [Export]
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public LoginViewModel(IEventAggregator eventAggregator)
        {
            LoginCommand = new DelegateCommand<object>(DoLogin);

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LoginCallbackEvent>().Subscribe(DoLoginCallbackEvent);

            SetWaitingVisibility(Visibility.Collapsed);
            SetLoginMessage("输入用户名和密码");
        }

        #region Fields
        [Import]
        public ILighterClientContext _lighterContext { get; set; }

        [Import]
        public ILoginCallback _loginCallback { get; set; }

        public string Title { get { return _lighterContext.GetClientName() + " 登陆界面"; } }

        public string ServerIp { get { return _lighterContext.GetServerIp(); } set { _lighterContext.WriteServerIp(value); } }

        public string ServerPort { get { return _lighterContext.GetServerPort(); } set { _lighterContext.WriteServerPort(value); } }

        public string LoginMessage { get; private set; }
        public void SetLoginMessage(string message)
        {
            LoginMessage = message;
            NotifyPropertyChanged("LoginMessage");
        }

        public Visibility WaitingVisibility { get; private set; }

        public void SetWaitingVisibility(Visibility visibility)
        {
            WaitingVisibility = visibility;

            NotifyPropertyChanged("WaitingVisibility");
        }
        #endregion Fields

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

        #region Login command
        public DelegateCommand<object> LoginCommand { get; private set; }
        public string Password { get; set; }
        private void DoLogin(object commandArg)
        {
            SetWaitingVisibility(Visibility.Visible);

            LoginInfo info = commandArg as LoginInfo;
            info.Password = Password;


            _lighterContext.AccountLogin(info, new InstanceContext(_loginCallback));
        }
        #endregion

        #region Login result event
        private void DoLoginCallbackEvent(LoginCallbackEventArgs args)
        {
            if (args.OpResult.ResultType != OperationResultType.Success)
                return;

            Account account = null;
            //if (args.Kind == LoginOperationKinds.Logout)
            //    _lighterContext.SetCurrentAccount(null);
            //else 
            if (args.Kind == LoginOperationKinds.Login)
            {
                string[] infos = args.OpResult.LogMessage.Split(new char[] { '|' });
                account = new Account(infos[0], infos[1], infos[2]);
            }

            _lighterContext.SetCurrentAccount(account);

            SetWaitingVisibility(Visibility.Collapsed);
        }
        #endregion
    }
}
