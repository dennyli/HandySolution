using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;
using Utility.Controls.Validators;

namespace Lighter.Client.Controls.Validators
{
    public class AccountValidationRule : NotNullorWhiteSpaceValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return base.Validate(value, cultureInfo);
        }
    }
}
