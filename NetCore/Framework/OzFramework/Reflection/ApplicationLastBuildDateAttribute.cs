/* File="ApplicationLastBuildDateAttribute"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace OzFramework.Reflection {
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

