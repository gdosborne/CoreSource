using System.Globalization;
using System.Windows.Controls;

namespace Life.Savings.Rules
{
    public class RequiredStringItemValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);
            if (string.IsNullOrEmpty(value.ToString()))
                validationResult = new ValidationResult(false, "String required.");
            return validationResult;
        }
    }
}
