using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GregOsborne.Application.Primitives {
	public static class Reflection {
#if !DOTNET3_5
		public static string GetPropertyName([CallerMemberName]string memberName = "") => memberName;
#endif
		public static string GetAssemblyAttributeValue<T>(this Assembly assembly, string propertyName) where T : Attribute {
			var attributes = assembly.GetCustomAttributes(typeof(T), false);
			if(attributes.Length > 0) {
				var descAttr = (T)attributes[0];
				var p = typeof(T).GetProperty(propertyName);
				return p.GetValue(descAttr, null).ToString();
			}
			return null;
		}
	}
}
