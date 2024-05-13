/* File="HeaderValueAttribute"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace OzFramework.Reflection {
    [AttributeUsage(AttributeTargets.Property)]
    public class HeaderValueAttribute : Attribute {
        internal HeaderValueAttribute() { }
        public HeaderValueAttribute(string headerValue) {
            HeaderValue = headerValue;
        }
        public string HeaderValue { get; set; }
    }
}
