using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using Lighter.Server.Infrastructure.Initialize;
using Lighter.ServerEvents;
using Lighter.ServiceManager;
using Lighter.ServiceManager.Hosting;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Utility;
using Microsoft.Practices.Prism.Commands;
using System.ServiceModel;
using Lighter.BaseService.Interface;

namespace Lighter.Server.ViewModel
{
    public class MainViewModel : NotificationObject
    {
        
        public MainViewModel()
        {
            Title = defaultTitle;
            ServerPort = 50000;
            Services = new ObservableCollection<ServiceInfo>();
            Users = new ObservableCollection<UserInfo>();

            HistoryMessages = new ObservableCollection<string>();

            LoadIniFile();
            FindServerInfo();
            StartupServices();

            OpenHistoryMessageCommand = new DelegateCommand(new Action(this.OpenHistoryMessageWindow));
        }

        public void ShutdownServices()
        {
            if (Services == null)
                return;

            try
            {
                foreach (ExportServiceHost host in _manager.Services)
                {
                    host.Close();

                    try
                    {
                        Services.Remove(Services.Single(s => s.Name == host.Meta.Name));
                    }
                    catch (InvalidOperationException e)
                    {
                    }
                }
            }
            catch (Exception e)
            {
                LastMessage = ExceptionMessage.GetExceptionMessage(e);
                RaisePropertyChanged("LastMessage");
            }
        }

        private void StartupServices()
        {
            try
            {
                LastMessage = "搜索可提供的服务...";
                RaisePropertyChanged("LastMessage");

                _bootstrapper = new ServiceHostBootstrapper();
                _bootstrapper.Run();

                //DatabaseInitializer.Initialize();

                IServiceLocator serviceLocator = _bootstrapper._container.GetExportedValue<IServiceLocator>();
                _manager = serviceLocator.GetInstance<IServiceHostManager>() as ServiceHostManager;
                _manager._container = _bootstrapper._container;
                _manager.LookupServices();

                LastMessage = "找到服务" + _manager.Services.Count<ExportServiceHost>().ToString() + "个";
                RaisePropertyChanged("LastMessage");

                foreach (ExportServiceHost host in _manager.Services)
                {
                    if (!host.UpdateAddressPort(ServerPort))
                    {
                        LastMessage = "Hosting Service: " + host.Meta.Name + " UpdateAddressPort " + ServerPort.ToString() + " failure.";
                        RaisePropertyChanged("LastMessage");
                    }

                    foreach (var address in host.Description.Endpoints)
                    {
                        host.Open();

                        LastMessage = "正在初始化服务 " + host.Meta.Name + " ... ";
                        RaisePropertyChanged("LastMessage");

                        OperationContext operationContext = OperationContext.Current;
                        InstanceContext instanceContext = operationContext.InstanceContext;
                        ILighterService service = instanceContext.GetServiceInstance() as ILighterService;
                        service.Initialize();

                        LastMessage = "服务已启动: " + host.Meta.Name + " at " + address.Address.Uri;
                        RaisePropertyChanged("LastMessage");

                        ServiceInfo info = new ServiceInfo(host.Meta.Name, address.Address.Uri);
                        Services.Add(info);
                        RaisePropertyChanged("Services");
                    }
                }

                LastMessage = "服务已提供";
                RaisePropertyChanged("LastMessage");

                IEventAggregator _eventAggregator = _manager._container.GetExportedValue<IEventAggregator>();
                _eventAggregator.GetEvent<AccountLoginEvent>().Subscribe(AccountLogined);
                _eventAggregator.GetEvent<AccountLogoutEvent>().Subscribe(AccountLogouted);
            }
            catch (Exception e)
            {
                LastMessage = ExceptionMessage.GetExceptionMessage(e);
                RaisePropertyChanged("LastMessage");
            }
        }

        private void AccountLogined(UserInfo user)
        {
            Users.Add(user);
        }

        private void AccountLogouted(string userName)
        {
            try
            {
                Users.Remove(Users.Single(u => u.Name == userName));
            }
            catch(Exception e)
            {
                LastMessage = ExceptionMessage.GetExceptionMessage(e);
                RaisePropertyChanged("LastMessage");
            }
        }

        private void FindServerInfo()
        {
            IPAddress address = GetHostIP();
            Byte[] bytes = address.GetAddressBytes();

            StringBuilder sb = new StringBuilder();
            foreach(Byte b in bytes)
            {
                if (sb.Length > 0)
                    sb.Append(".");

                sb.Append(b);
            }

            ServerIP = sb.ToString();
            RaisePropertyChanged("ServerIP");

            ServerName = Dns.GetHostName();
            RaisePropertyChanged("ServerName");
        }

        private IPAddress GetHostIP()
        {
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = ipe.AddressList[0];

            return ip;
        }

        private void LoadIniFile()
        {
            string iniName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.ini");
            try
            {
                Title = OperatorFile.GetIniFileString("server", "name", defaultTitle, iniName);
            }
            catch (Exception e)
            {
                Title = defaultTitle;
            }
        }

        /// <summary>
        /// 服务器界面标题
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIP { get; private set; }
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; private set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort { get; private set; }
        /// <summary>
        /// 历史消息
        /// </summary>
        public ObservableCollection<string> HistoryMessages { get; private set; }
        public string LastMessage { get; set; }
        /// <summary>
        /// 服务列表
        /// </summary>
        public ObservableCollection<ServiceInfo> Services { get; private set; }

        public ObservableCollection<UserInfo> Users { get; private set; }

        /// <summary>
        /// Service Host Startup & Manager
        /// </summary>
        private ServiceHostBootstrapper _bootstrapper = null;
        private ServiceHostManager _manager = null;
        /// <summary>
        /// 系统默认的标题
        /// </summary>
        private string defaultTitle = "订单管理系统 (服务端)";

        public DelegateCommand OpenHistoryMessageCommand { get; private set; }
        private void OpenHistoryMessageWindow()
        {
            HistoryMessageWindow hmWnd = new HistoryMessageWindow(this);
            hmWnd.ShowDialog();
        }

        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);

            if (propertyName == "LastMessage")
                HistoryMessages.Add(LastMessage);
        }

    }
}
