using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace Utility.Controls.Validators
{
    public class NotNullorWhiteSpaceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "输入值不能为空");

            if (typeof(string) == value.GetType())
            {
                string valString = value as string;
                if (string.IsNullOrWhiteSpace(valString))
                    return new ValidationResult(false, "输入值不能为空");
            }

            return new ValidationResult(true, "");
        }
    }
}
