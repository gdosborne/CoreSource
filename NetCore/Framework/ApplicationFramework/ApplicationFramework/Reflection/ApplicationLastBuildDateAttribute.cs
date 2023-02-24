using System;

namespace Common.Application.Reflection {
    [System.AttributeUsage(AttributeTargets.Assembly)]
    public class ApplicationLastBuildDateAttribute : Attribute {
        public ApplicationLastBuildDateAttribute(string buildDate) {
            var date = DateTime.MinValue;
            if (DateTime.TryParse(buildDate, out date)) {
                BuildDate = date;
            }
        }

        public DateTime BuildDate { get; private set; } = default;
    }
}

