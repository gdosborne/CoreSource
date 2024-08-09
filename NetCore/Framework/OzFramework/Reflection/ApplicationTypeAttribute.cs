/* File="ApplicationTypeAttribute"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace Common.Reflection {
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

