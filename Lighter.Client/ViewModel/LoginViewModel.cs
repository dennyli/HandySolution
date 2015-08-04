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

            SetWaiting(Visibility.Collapsed);
        }

        [Import]
        public ILighterClientContext _lighterContext { get; set; }

        [Import]
        public ILoginCallback _loginCallback { get; set; }

        public string Title { get { return "订单系统登陆界面"; } }

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
        public string LoginMessage { get; set; }
        public Visibility Waiting { get; private set; }

        public void SetWaiting(Visibility visibility)
        {
            Waiting = visibility;
        }

        private void DoLogin(object commandArg)
        {
            SetWaiting(Visibility.Visible);

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

            SetWaiting(Visibility.Collapsed);
        }
        #endregion
    }
}
