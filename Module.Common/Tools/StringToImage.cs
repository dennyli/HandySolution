using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows.Controls;

namespace Client.Module.Common.Tools
{
    public class StringToImage : IValueConverter
    {
        private static readonly StringToImage defaultInstance = new StringToImage();
        public static StringToImage Default { get { return defaultInstance; } }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = (string)value;

            Image im = new Image();
            im.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            im.Stretch = System.Windows.Media.Stretch.UniformToFill;
            return im;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
