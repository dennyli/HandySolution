using System.ComponentModel;
using System.ComponentModel.Composition;
using Lighter.Client.Infrastructure.Implement;
using Lighter.Client.Infrastructure.Interface;
using Lighter.LoginService.Interface;
using Lighter.LoginService.Model;
using Microsoft.Practices.Prism.Commands;
using Lighter.MainService.Interface;
using Utility;
using Microsoft.Practices.Prism.Events;
using Lighter.Client.Events;
using System.ServiceModel;
using Lighter.UserManagerService.Model;
using System.Windows;

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

        [Import]
        public ILighterClientContext _lighterContext { get; set; }

        [Import]
        public ILoginCallback _loginCallback { get; set; }

        public string Title { get { return "订单系统登陆界面"; } }

        public string ServerIp { get { return _lighterContext.GetServerIp(); } set { _lighterContext.WriteServerIp(value); } }

        public string ServerPort { get { return _lighterContext.GetServerPort(); } set { _lighterContext.WriteServerPort(value); } }

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

        private void DoLogin(object commandArg)
        {
            SetWaitingVisibility(Visibility.Visible);

            LoginInfo info = commandArg as LoginInfo;

            ILighterLoginService service = _lighterContext.FindService(ServiceFactory.LOGIN_SERVICE_NAME) as ILighterLoginService;
            if (service == null)
            {
                InstanceContext contextCallback = new InstanceContext(_loginCallback);
                service = _lighterContext.CreateServiceByMainService<ILighterLoginService>(ServiceFactory.LOGIN_SERVICE_NAME, contextCallback, false);
            }

            service.Login(info);
        }
        #endregion

        #region Login result event
        private void DoLoginCallbackEvent(LoginCallbackEventArgs args)
        {
            if (args.OpResult.ResultType != OperationResultType.Success)
                return;

            AccountDTO dto = null;
            //if (args.Kind == LoginOperationKinds.Logout)
            //    _lighterContext.SetCurrentAccount(null);
            //else 
            if (args.Kind == LoginOperationKinds.Login)
            {
                string[] infos = args.OpResult.LogMessage.Split(new char[] { '|' });
                dto = new AccountDTO();
                dto.Id = infos[0];
                dto.Name = infos[1];
                dto.Authority = infos[2];
            }

            _lighterContext.SetCurrentAccount(dto);

            SetWaitingVisibility(Visibility.Collapsed);
        }
        #endregion
    }
}
