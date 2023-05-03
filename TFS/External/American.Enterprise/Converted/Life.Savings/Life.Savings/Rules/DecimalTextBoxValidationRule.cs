using System.Globalization;
using System.Windows.Controls;

namespace Life.Savings.Rules
{
    public class DecimalTextBoxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);
            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    value = ((string)value).Replace("$", string.Empty);
                    if (double.TryParse(value.ToString(), out var actaulValue))
                        return validationResult;
                    validationResult = new ValidationResult(false, "Illegal Characters, Please Enter Numeric Value");
                }

            }
            return validationResult;
        }
    }
}
