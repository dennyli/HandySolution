using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Lighter.LoginService.Model;
using Utility;
using System.Diagnostics;
using System.Windows.Controls;
using Lighter.Client.ViewModel;

namespace Lighter.Client.Converters
{
    public class LoginInfoConverter : IMultiValueConverter
    {
        #region IMultiValueConverter 成员

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debug.Assert(values.Count() == 2);
            Debug.Assert(values[0] is TextBox);
            Debug.Assert(values[1] is PasswordBox);
            if (!(values[0] is TextBox) || !(values[1] is PasswordBox))
                return null;

//            string account = (values[0] as TextBox).Text;
//#if DEBUG
//            string pwd = (values[1] as PasswordBox).Password;
//#else
//            string pwd = (values[1] as PasswordBox).SecurePassword;
//#endif
//            LoginInfo info = new LoginInfo(account, pwd, CommonUtility.GetHostIP4v());

            LoginInfoArgs info = new LoginInfoArgs(values[0] as TextBox, values[1] as PasswordBox, CommonUtility.GetHostIP4v());

            return info;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
