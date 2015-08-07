using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Lighter.Client.Events.LoginEvents;
using Lighter.Client.Infrastructure.Events.ServiceEvents;
using Lighter.Client.ViewModel;
using Microsoft.Practices.Prism.Events;
using Utility;
using Utility.Controls;
using Lighter.Client.Events.InputEvents;

namespace Lighter.Client.View
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeImageBrushs();
            //InitializeEventAggregator();
        }

        #region Property

        [Import(AllowRecomposition = false)]
        public LoginViewModel ViewModel
        {
            get { return this.DataContext as LoginViewModel; }
            set { this.DataContext = value; }
        }

        [Import]
        public IEventAggregator EventAggregator { get; set; }

        [Import]
        public ILoginCallback LoginCallback { get; set; }
        #endregion

        #region Login result event
        public void InitializeEventAggregator()
        {
            EventAggregator.GetEvent<LoginCallbackEvent>().Subscribe(DoLoginCallbackEvent);
            EventAggregator.GetEvent<ServiceEvent>().Subscribe(DoServiceEvent);
            EventAggregator.GetEvent<InputErrorEvent>().Subscribe(DoInputErrorEvent);
        }

        private void DoInputErrorEvent(InputErrorEventArgs args)
        {
            ViewModel.SetLoginMessage(args.Message);
            LighterMessageBox.ShowMessageBox(this, args.Message, "提示");
        }

        private void DoServiceEvent(ServiceEventArgs args)
        {
            switch (args.Kind)
            {
                case ServiceEventKind.TooBusy:
                case ServiceEventKind.NotFound:
                    ViewModel.SetLoginMessage(args.Message);
                    LighterMessageBox.ShowMessageBox(this, args.Message, "提示");
                    break;
                case ServiceEventKind.Closed:
                    break;
            }
        }

        private void DoLoginCallbackEvent(LoginCallbackEventArgs args)
        {
            switch (args.Kind)
            {
                case LoginOperationKinds.Login:
                    switch(args.OpResult.ResultType)
                    {
                        case OperationResultType.Success:
                            ViewModel.SetLoginMessage("登录成功!");
                            this.DialogResult = true;
                            break;
                        case OperationResultType.IsLogined:
                            {
                                string message = "账户" + txtAccount.Text + "已登录, 不能重复登录!";

                                ViewModel.SetLoginMessage(message);
                                LighterMessageBox.ShowMessageBox(this, message, "提示");

                                //this.DialogResult = false;
                            }
                            break;
                        default:
                            {
                                string message = "账户" + txtAccount.Text + "登录失败!";

                                ViewModel.SetLoginMessage(message);
                                LighterMessageBox.ShowMessageBox(this, message, "提示");

                                //this.DialogResult = false;
                            }
                            break;
                    }

                    
                    break;
                //case LoginOperationKinds.Logout:
                //    if (args.OpResult.ResultType == OperationResultType.Success)
                //        ViewModel.SetLoginMessage("退出成功");
                //    else
                //        ViewModel.SetLoginMessage("退出失败");
                //    break;
                //default:
                //    ViewModel.SetLoginMessage("未知信息");
                //    break;
            }
        }
        #endregion

        #region Initialize Image Brushs
        private ImageBrush ibCancelNormal, ibCancelFocus;
        private ImageBrush ibLoginNormal, ibLoginFocus;
        private void InitializeImageBrushs()
        {
            ibCancelNormal = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Lighter.Client;component/Images/cancel.png")));
            ibCancelNormal.Stretch = Stretch.Fill;

            ibCancelFocus = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Lighter.Client;component/Images/cancel_focus.png")));
            ibCancelFocus.Stretch = Stretch.Fill;

            ibLoginNormal = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Lighter.Client;component/Images/btn_background.gif")));
            ibLoginNormal.Stretch = Stretch.Fill;

            ibLoginFocus = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Lighter.Client;component/Images/btn_background_focus.gif")));
            ibLoginFocus.Stretch = Stretch.Fill;
        }
        #endregion

        #region Events
        private void cbServerParam_Click(object sender, RoutedEventArgs e)
        {
            panelIP.Visibility = cbServerParam.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            panelPort.Visibility = cbServerParam.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;

            if (panelPort.Visibility == Visibility.Collapsed)
                this.Height = 330;
            else
                this.Height = 355;
        }

        private void tips_close_MouseEnter(object sender, MouseEventArgs e)
        {
            Label tipsLabel = (Label)sender;
            try
            {
                tipsLabel.Background = ibCancelFocus;
            }
            catch (Exception ef)
            {
                LighterMessageBox.ShowMessageBox(this, "出现错误！：" + ef.ToString(), "提示");
            }

        }

        private void tips_close_MouseLeave(object sender, MouseEventArgs e)
        {
            Label tipsLabel = (Label)sender;
            try
            {
                tipsLabel.Background = ibCancelNormal;
            }
            catch (Exception ef)
            {
                LighterMessageBox.ShowMessageBox(this, "出现错误！：" + ef.ToString(), "提示");
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

            this.Close();
        }

        private void tips_login_MouseEnter(object sender, MouseEventArgs e)
        {
            Label tipsLabel = (Label)sender;
            try
            {
                tipsLabel.Background = ibLoginFocus;
            }
            catch (Exception ef)
            {
                LighterMessageBox.ShowMessageBox(this, "出现错误！：" + ef.ToString(), "提示");
            }

        }

        private void tips_login_MouseLeave(object sender, MouseEventArgs e)
        {
            Label tipsLabel = (Label)sender;
            try
            {
                tipsLabel.Background = ibLoginNormal;
            }
            catch (Exception ef)
            {
                LighterMessageBox.ShowMessageBox(this, "出现错误！：" + ef.ToString(), "提示");
            }

        }

        //private void btnTest_Click(object sender, RoutedEventArgs e)
        //{
        //    LighterMessageBox.ShowMessageBox(this, "托尔斯泰", "提示");
        //}

        #endregion
    }
}
