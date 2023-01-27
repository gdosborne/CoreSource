using System;

namespace Common.Applicationn.Reflection {
    [System.AttributeUsage(AttributeTargets.Assembly)]
    public class ApplicationTypeAttribute : Attribute {
        public enum ApplicationTypes {
            Console,
            Desktop,
            UWP,
            ClassLibrary
        }

        public ApplicationTypeAttribute(ApplicationTypes appType) {
            ApplicationType = appType;
        }

        public ApplicationTypes ApplicationType { get; private set; } = ApplicationTypes.Console;

    }
}

