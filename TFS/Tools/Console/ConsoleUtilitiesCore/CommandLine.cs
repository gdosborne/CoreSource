namespace ConsoleUtilities {
    using System.Collections.Generic;
    using System.Reflection;

    public static class CommandLine {
        private const char equalsSign = '=';
        private static char[] trimChars = null;

        //if values have spaces, encase in double quotes

        public static Dictionary<string, string> GetArguments(string[] args, out string executablePath) {
            //must have a space between arguments
            //if there is a value then it must be = sign between the key and value, no spaces

            executablePath = Assembly.GetEntryAssembly().Location;
            var result = new Dictionary<string, string>();
            for (var i = 0; i < args.Length; i++) {
                var parts = args[i].Split('=');
                if (parts.Length == 2) {
                    result.Add(parts[0], parts[1]);
                }
                else {
                    result.Add(parts[0], null);
                }
            }
            return result;
        }

        public static Dictionary<string, string> GetArguments(string commandLine, char commandSeparator, out string executablePath) {
            var result = new Dictionary<string, string>();

            var args = commandLine.Split(commandSeparator, StringSplitOptions.RemoveEmptyEntries);
            executablePath = args[0];
            if (args.Length > 1) {
                var values = args[1].Split(equalsSign, StringSplitOptions.RemoveEmptyEntries);
                result.Add(values[0], values[1]);
            }
            return result;
        }
        //public static Dictionary<string, string> GetArguments(string commandLine, char commandSeparator, out string executablePath) {
        //	var result = new Dictionary<string, string>();
        //	executablePath = default;
        //	//arguments don't necessarily have to have spaces between
        //	//if there is a value then it must be = sign between the key and value, no spaces

        //	var lowerCommandLine = commandLine.ToLowerInvariant();

        //	if (lowerCommandLine.Replace("'", string.Empty).Replace("\"", string.Empty).Trim().Equals(Assembly.GetEntryAssembly().Location.ToLower(), System.StringComparison.OrdinalIgnoreCase)) {
        //		executablePath = Assembly.GetEntryAssembly().Location;
        //		return result;
        //	} else {
        //		//check for file name passed
        //		var lowerTest = lowerCommandLine.Replace("\"", string.Empty).Replace(Assembly.GetEntryAssembly().Location.ToLower(), string.Empty).Trim();
        //	}

        //	var temp = string.Empty;
        //	var newItem = false;
        //	var hasParameterId = false;
        //	var skipEquals = false;
        //	trimChars = new char[] { ' ', '"', '\'' };
        //	var parameterId = ' ';
        //	var tempExecutablePath = string.Empty;
        //	var chars = commandLine.ToCharArray();

        //	for (var i = 0; i < chars.Length; i++) {
        //		if (newItem) {
        //			if (!hasParameterId) {
        //				parameterId = chars[i];
        //				hasParameterId = true;
        //			} else {
        //				newItem = false;
        //				hasParameterId = false;
        //				skipEquals = true;
        //			}
        //		}
        //		if (chars[i] == commandSeparator) {
        //			if (string.IsNullOrWhiteSpace(tempExecutablePath)) {
        //				tempExecutablePath = temp.Trim(trimChars);
        //				temp = string.Empty;
        //				newItem = true;
        //			} else if (chars.Length == i + 2) {
        //				parameterId = chars[i + 1];
        //				break;
        //			}
        //			if (i + 2 < chars.Length && chars[i + 2] == equalsSign) {
        //				if (string.IsNullOrWhiteSpace(tempExecutablePath)) {
        //					tempExecutablePath = temp.Trim(trimChars);
        //				} else {
        //					result.Add((commandSeparator.ToString() + parameterId.ToString()), temp.Trim(trimChars));
        //				}

        //				temp = string.Empty;
        //				newItem = true;
        //			} else if (string.IsNullOrWhiteSpace(tempExecutablePath)) {
        //				tempExecutablePath = temp.Trim(trimChars);
        //				temp = string.Empty;
        //				newItem = true;
        //			} else if (chars[i] != commandSeparator) {
        //				temp += chars[i];
        //			}
        //		} else if (!skipEquals && !hasParameterId) {
        //			temp += chars[i];
        //		}

        //		skipEquals = false;
        //	}
        //	if (string.IsNullOrWhiteSpace(tempExecutablePath)) {
        //		tempExecutablePath = temp.Trim(trimChars);
        //	} else if (parameterId != ' ') {
        //		result.Add((commandSeparator.ToString() + parameterId.ToString()), temp.Trim(trimChars));
        //	}

        //	executablePath = tempExecutablePath;
        //	return result;
        //}
    }
}
