using System.Collections.Generic;

namespace OzDB.Management.Validators {
    public class ValidatorBase : IValidator {
        public enum ValidationTypes {
            IsInMinMax,
            IsNotInMinMax,
            IsInValueList,
            IsNotInValueList,
            RegEx
        }

        public ValidatorBase(dynamic min, dynamic max, bool isInclusive = true) {
            ValidationType = isInclusive ? ValidationTypes.IsInMinMax : ValidationTypes.IsNotInMinMax;
            MinValue = min;
            MaxValue = max;
        }

        public ValidatorBase(List<dynamic> values, bool isInclusive = true) {
            ValidationType = isInclusive ? ValidationTypes.IsInValueList : ValidationTypes.IsNotInValueList;
            ValueList = values;
        }

        public ValidatorBase(string regEx) {
            ValidationType = ValidationTypes.RegEx;
            RegularExpression = regEx;
        }

        public virtual dynamic? MinValue { get; set; }
        public virtual dynamic? MaxValue { get; set; }
        public virtual List<dynamic>? ValueList { get; set; }
        public virtual string? RegularExpression { get; set; }
        public ValidationTypes ValidationType { get; set; }

        public virtual bool IsValid(dynamic value) { return false; }
    }
}
