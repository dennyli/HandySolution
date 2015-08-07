using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Utility.Controls.Validators;

namespace Lighter.Client.Controls.Validators
{
    internal class ServcerIPValidationRule : NotNullorWhiteSpaceValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string))
                return  new ValidationResult(false, "服务端IP地址格式不正确");

            ValidationResult vr = base.Validate(value, cultureInfo);
            if (!vr.IsValid)
                return vr;

            string num = "(25[0-5]|2[0-4]//d|[0-1]//d{2}|[1-9]?//d)";
            if(!Regex.IsMatch(value as string, ("^" + num + "//." + num + "//." + num + "//." + num + "$")))
                return new ValidationResult(false, "服务端IP地址格式不正确");

            return new ValidationResult(true, "");
        }
    }
}
