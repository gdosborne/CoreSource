namespace ConsoleUtilities {
	using System;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using GregOsborne.Application;

	public static class Helper {
		public static Tuple<T1, T2> GetTuple<T1, T2>(T1 value1, T2 value2) => new Tuple<T1, T2>(value1, value2);

		public static string Description<T>(this T source) where T : struct, IConvertible {
			if (!typeof(T).IsEnum) {
				return source.ToString();
			}

			var fi = source.GetType().GetField(source.ToString());
			var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes != null && attributes.Length > 0
				? attributes[0].Description
				: source.ToString();
		}

		public static string Replace(this string source, params Tuple<string, string>[] with) {
			with.ToList().ForEach(x => source = source.Replace(x.Item1, x.Item2));
			return source;
		}
		static Helper() => EmailBodyText = new StringBuilder(Convert.ToInt32(ByteSize.BytesInMegaByte * 5));

		public static bool HasTextForEMail {
			get; set;
		}

		public static StringBuilder EmailBodyText {
			get; private set;
		}

		public static Version GetVersion() {
			var assy = Assembly.GetEntryAssembly();
			return assy.GetName().Version;
		}

		public static string GetTitle() => ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(), typeof(AssemblyTitleAttribute), false)).Title;

		public static string Space(int number) {
			if (number == 0) {
				return string.Empty;
			}

			return new string(' ', number);
		}
	}
}
