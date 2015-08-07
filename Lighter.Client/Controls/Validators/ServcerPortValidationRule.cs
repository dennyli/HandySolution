using System.Globalization;
using System.Windows.Controls;
using Utility.Controls.Validators;
using System.Text.RegularExpressions;

namespace Lighter.Client.Controls.Validators
{
    internal class ServcerPortValidationRule : NotNullorWhiteSpaceValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string))
                return new ValidationResult(false, "服务端Port格式不正确");

            ValidationResult vr = base.Validate(value, cultureInfo);
            if (!vr.IsValid)
                return vr;

            string num = "^//d+$";
            if (!Regex.IsMatch(value as string, num))
                return new ValidationResult(false, "服务端Port格式不正确");

            return new ValidationResult(true, "");
        }
    }
}
