using System;
using System.Globalization;
using System.Windows.Controls;

namespace Life.Savings.Rules
{
    public class BirthDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);
            if (((DateTime?)value).HasValue)
            {
                if (DateTime.Now < ((DateTime?)value).Value)
                    validationResult = new ValidationResult(false, "Value must be less than current date/time.");
            }
            return validationResult;
        }
    }
}
