using System.Globalization;
using System.Windows.Controls;
using Utility.Controls.Validators;

namespace Lighter.Client.Controls.Validators
{
    internal class PasswordValidationRule : NotNullorWhiteSpaceValidationRule
    {
        public static bool Validate(string value)
        {
            PasswordValidationRule rule = new PasswordValidationRule();
            return rule.Validate(value, CultureInfo.CurrentCulture).IsValid;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult vr = base.Validate(value, cultureInfo);
            if (!vr.IsValid)
                return vr;

            if (typeof(string) == value.GetType())
            {
                string valString = value as string;
                int length = valString.Length;
                if ((length <= 0) || (length > 16))
                    return new ValidationResult(false, "密码长度超长!");
            }

            return new ValidationResult(true, "");
        }
    }
}
