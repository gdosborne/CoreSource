
namespace ConsoleHelpers {
	using System.Collections.Generic;
	using System.Linq;

	public static class CommandLine {
		private const char equalsSign = '=';

		public static Dictionary<string, string> GetArguments(string commandLine, char commandSeparator, out string executablePath) {
			var result = new Dictionary<string, string>();
			var trimChars = new char[] { ' ', '"', '\'' }; ;
			var newItem = false;
			var hasParameterId = false;
			var skipEquals = false;
			var parameterId = ' ';
			var tempExecutablePath = string.Empty;
			var temp = string.Empty;

			var chars = commandLine.ToCharArray();
			for (var i = 0; i < chars.Length; i++) {
				if (newItem) {
					if (!hasParameterId) {
						parameterId = chars[i];
						hasParameterId = true;
					} else {
						newItem = false;
						hasParameterId = false;
						skipEquals = true;
					}
				}
				if (chars[i] == commandSeparator) {
					if (chars.Length == i + 2) {
						parameterId = chars[i + 1];
						break;
					}
					if (chars[i + 2] == equalsSign) {
						if (string.IsNullOrEmpty(tempExecutablePath)) {
							tempExecutablePath = temp.Trim(trimChars);
						} else {
							result.Add((commandSeparator.ToString() + parameterId.ToString()), temp.Trim(trimChars));
						}

						temp = string.Empty;
						newItem = true;
					} else if (string.IsNullOrEmpty(tempExecutablePath)) {
						tempExecutablePath = temp.Trim(trimChars);
						temp = string.Empty;
						newItem = true;
					} else {
						temp += chars[i];
					}
				} else if (!skipEquals && !hasParameterId) {
					temp += chars[i];
				}

				skipEquals = false;
			}
			if (parameterId != ' ') {
				result.Add((commandSeparator.ToString() + parameterId.ToString()), temp.Trim(trimChars));
			}

			var args = new List<string>();
			foreach (var item in result) {
				args.Add($"{item.Key}={item.Value}");
			}
			executablePath = tempExecutablePath;
			return GetArguments(args.ToArray(), commandSeparator);
		}

		public static Dictionary<string, string> GetArguments(string[] args, char commandSeparator) {
			var result = new Dictionary<string, string>();
			args.ToList().ForEach(x => {
				var parts = x.Split('=');
				var key = parts[0];
				var value = parts[1];
				result.Add(key, value);
			});
			return result;
		}
	}
}

