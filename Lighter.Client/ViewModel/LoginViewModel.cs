using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.Windows;
using Lighter.Client.Controls.Validators;
using Lighter.Client.Events.InputEvents;
using Lighter.Client.Events.LoginEvents;
using Lighter.Client.Infrastructure.Accounts;
using Lighter.Client.Infrastructure.Events.ServiceEvents;
using Lighter.Client.Infrastructure.Interface;
using Lighter.LoginService.Model;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Utility;
using Utility.Exceptions;

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
            SetEnabled(true);
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

        public bool IsEnabled { get; private set; }
        private void SetEnabled(bool bEnabled)
        {
            IsEnabled = bEnabled;
            NotifyPropertyChanged("IsEnabled");
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
        //public string Password { get; set; }

        private void SetLoginState(bool bLogin)
        {
            SetEnabled(!bLogin);
            SetWaitingVisibility(bLogin ? Visibility.Visible : Visibility.Collapsed);
        }

        private void DoLogin(object commandArg)
        {
            SetLoginState(true);

            LoginInfoArgs infoArgs = commandArg as LoginInfoArgs;

            bool bValidated = true;
            if (!AccountValidationRule.Validate(infoArgs.AccountControl.Text))
            {
                _eventAggregator.GetEvent<InputErrorEvent>().Publish(new InputErrorEventArgs(InputErrorKinds.Account));
                bValidated = false;
            }

#if DEBUG
            if (!PasswordValidationRule.Validate(infoArgs.PasswordControl.Password))
#else
            if (!PasswordValidationRule.Validate(infoArgs.PasswordControl.SecurePassword))
#endif
            {
                _eventAggregator.GetEvent<InputErrorEvent>().Publish(new InputErrorEventArgs(InputErrorKinds.Password));
                bValidated = false;
            }

            if (bValidated)
            {
                try
                {
#if DEBUG
                    LoginInfo info = new LoginInfo(infoArgs.AccountControl.Text, infoArgs.PasswordControl.Password, infoArgs.IP);
#else
                    LoginInfo info = new LoginInfo(infoArgs.AccountControl.Text, infoArgs.PasswordControl.SecurePassword, infoArgs.IP);
#endif
                    _lighterContext.AccountLogin(info, new InstanceContext(_loginCallback));
                }
                catch (ServerClosedException ex)
                {
                    SetLoginMessage(ex.Message);
                    _eventAggregator.GetEvent<ServiceEvent>().Publish(new ServiceEventArgs(ServiceEventKind.Closed, ex.Message));
                }
                catch (ServerNotFoundException ex)
                {
                    SetLoginMessage(ex.Message);

                    _eventAggregator.GetEvent<ServiceEvent>().Publish(new ServiceEventArgs(ServiceEventKind.NotFound, ex.Message));
                }
                catch (ServerTooBusyException ex)
                {
                    SetLoginMessage(ex.Message);
                    _eventAggregator.GetEvent<ServiceEvent>().Publish(new ServiceEventArgs(ServiceEventKind.TooBusy, ex.Message));
                }
            }

            SetLoginState(false);
        }
        #endregion

        #region Login result event
        private void DoLoginCallbackEvent(LoginCallbackEventArgs args)
        {
            SetLoginState(false);

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
        }
        #endregion
    }
}
