using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;
using Utility.Controls.Validators;

namespace Lighter.Client.Controls.Validators
{
    internal class AccountValidationRule : NotNullorWhiteSpaceValidationRule
    {
        public static bool Validate(string value)
        {
            AccountValidationRule rule = new AccountValidationRule();
            return rule.Validate(value, CultureInfo.CurrentCulture).IsValid;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return base.Validate(value, cultureInfo);
        }
    }
}
