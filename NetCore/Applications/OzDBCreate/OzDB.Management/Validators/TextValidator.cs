using Common.Application.Primitives;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OzDB.Management.Validators {
    public class TextValidator : ValidatorBase {
        public TextValidator(string min, string max, bool isInclusive)
            : base(min, max, isInclusive) { }

        public TextValidator(List<string> values, bool isInclusive)
            : base(values, isInclusive) { }

        public TextValidator(string regEx)
            : base(regEx) { }

        public override bool IsValid(dynamic value) {
            var result = true;
            result = value != null && value.Is<double>();
            if (!result) {
                return result;
            }

            try {
                result = ValidationType switch {
                    ValidationTypes.IsInMinMax => (bool)(value >= MinValue && value <= MaxValue),
                    ValidationTypes.IsNotInMinMax => (bool)!(value >= MinValue && value <= MaxValue),
                    ValidationTypes.IsInValueList => (bool)(ValueList != null && ValueList.Contains(value)),
                    ValidationTypes.IsNotInValueList => (bool)(ValueList == null || !ValueList.Contains(value)),
                    ValidationTypes.RegEx => (bool)Regex.IsMatch(value, RegularExpression),
                    _ => false,
                };
            }
            catch (System.Exception) {
                result = false;
            }
            return result;
        }

        public override dynamic MaxValue { get; set; }
        public override dynamic MinValue { get; set; }
        public override List<dynamic>? ValueList { get; set; }
        public override string? RegularExpression { get; set; }
    }
}
