using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Lighter.LoginService.Model;
using Utility;

namespace Lighter.Client.Converters
{
    public class LoginInfoConverter : IMultiValueConverter
    {
        #region IMultiValueConverter 成员

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            LoginInfo info = new LoginInfo(values[0] as string, values[1] as string, CommonUtility.GetHostIP4vDotFormat());

            return info;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
