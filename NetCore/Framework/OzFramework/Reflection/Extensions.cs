/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Universal.Common;

namespace OzFramework.Reflection {
    public static class Extensions {
        public static List<TA> GetAttributes<T, TA>(this T value) where T : class where TA : Attribute {
            var result = new List<TA>();
            if (value.IsNull()) return result;
            var props = value.GetType().GetProperties().Where(x => !x.GetCustomAttribute(typeof(TA)).IsNull());
            if (props.Any()) {
                props.ForEach(prop => {
                    result.Add(prop.GetCustomAttribute(typeof(TA)).As<TA>());
                });
            }
            return result;
        }

        public static string GetPropertyName([CallerMemberName] string caller = null) => caller;

        public static string Description(this PropertyInfo pInfo, bool noSubstitues = false) {
            if (pInfo.IsNull()) {
                throw new ArgumentNullException(nameof(pInfo));
            }
            var attrs = pInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs.IsNull() || !attrs.Any()) {
                if (noSubstitues) {
                    return default;
                }
                return pInfo.Name;
            }
            return attrs[0].As<DescriptionAttribute>().Description;
        }

        public static string Description(this Type type, string propertyName, bool noSubstitues = false) {
            if (type.IsNull()) {
                throw new ArgumentNullException(nameof(type));
            }
            if (propertyName.IsNull()) {
                throw new ArgumentNullException(nameof(propertyName));
            }
            var pInfo = type.GetProperty(propertyName);
            if (pInfo.IsNull()) {
                return propertyName;
            }
            return pInfo.Description(noSubstitues);
        }

    }
}
