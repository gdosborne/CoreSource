/* File="EnumMetaDataAttribute"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="4/25/2024" */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzFramework.Attributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumMetaDataAttribute : Attribute {
        public EnumMetaDataAttribute(string description, bool isErrorValue = false, string otherName = default) {
            Description = description;
            IsErrorValue = isErrorValue;
            OtherName = otherName;
        }
        public string Description { get; private set; }
        public bool IsErrorValue { get; private set; }
        public string OtherName { get; private set; }
    }
}
