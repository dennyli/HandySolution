using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.ComponentModel.Composition;
using Lighter.Client.ViewModel;
using Microsoft.Practices.Prism.Events;
using Lighter.Client.Events;
using Utility;

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


        #region Login result event
        public void InitializeEventAggregator()
        {
            EventAggregator.GetEvent<LoginCallbackEvent>().Subscribe(DoLoginCallbackEvent);
        }

        private void DoLoginCallbackEvent(LoginCallbackEventArgs args)
        {
            switch (args.Kind)
            {
                case LoginOperationKinds.Login:
                    if (args.OpResult.ResultType == OperationResultType.Success)
                    {
                        ViewModel.SetLoginMessage("登录成功");

                        this.DialogResult = true;

                        this.Close();
                    }
                    else
                        ViewModel.SetLoginMessage("登录失败");
                    break;
                case LoginOperationKinds.Logout:
                    if (args.OpResult.ResultType == OperationResultType.Success)
                        ViewModel.SetLoginMessage("退出成功");
                    else
                        ViewModel.SetLoginMessage("退出失败");
                    break;
                default:
                    ViewModel.SetLoginMessage("未知信息");
                    break;
            }
        }
        #endregion

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
                MessageBox.Show("出现错误！：" + ef.ToString());
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
                MessageBox.Show("出现错误！：" + ef.ToString());
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
                MessageBox.Show("出现错误！：" + ef.ToString());
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
                MessageBox.Show("出现错误！：" + ef.ToString());
            }

        }
    }
}
