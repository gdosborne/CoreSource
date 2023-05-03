using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OSoftFormat
{
	internal enum MessageTypes
	{
		Information,
		Warning,
		Error
	}
	[Flags]
	internal enum ExitCodes
	{
		None = 0,
		CodeFileNotSpecified = 16,
		RuleFileNotSpecified = 32
	}
	internal class Program
	{

		private static void WriteHeader()
		{
			WriteLine("OSoft Code Formatter");
			WriteFormat("Version {0}", Assembly.GetExecutingAssembly().GetName().Version);
			WriteLine();
		}

		private static string GetFileNameFromParameter(string[] args, string paramId)
		{
			var p = args.FirstOrDefault(x => x.StartsWith(paramId, StringComparison.OrdinalIgnoreCase));
			if (p == null)
				return null;
			var fileName = p.Split('=')[1];
			if (!File.Exists(fileName))
				return null;
			return fileName;
		}

		private static void Main(string[] args)
		{
			WriteHeader();
			if (args.Any(x => x.Equals("/?", StringComparison.OrdinalIgnoreCase)) || args.Any(x => x.Equals("/h", StringComparison.OrdinalIgnoreCase)))
			{
				WriteHelp();
				return;
			}

			ExitCodes exitCode = ExitCodes.None;
			var fileNameToProcess = GetFileNameFromParameter(args, "/f");
			var rulesFileName = GetFileNameFromParameter(args, "/r");

			if (string.IsNullOrEmpty(fileNameToProcess))
			{
				WriteLine(MessageTypes.Error, "No code file specified or cannot find the code file.");
				exitCode = ExitCodes.CodeFileNotSpecified;
			}

			if (string.IsNullOrEmpty(fileNameToProcess))
			{
				WriteLine(MessageTypes.Error, "No rules file specified or cannot find the rules file.");
				exitCode = exitCode | ExitCodes.RuleFileNotSpecified;
			}

			if (exitCode != ExitCodes.None)
			{
#if DEBUG
				Console.ReadKey();
#endif
				Environment.Exit((int)exitCode);
			}

			WriteFormat("Formatting {0}", fileNameToProcess);
			WriteFormat("Using {0}", rulesFileName);

#if DEBUG
			Console.ReadKey();
#endif
		}

		private static void WriteHelp()
		{
			WriteLine("Help");
			WriteLine();

			Write("/f".PadRight(10));
			WriteLine("Code file name");

			Write("/r".PadRight(10));
			WriteLine("Rules file name");

			WriteLine();
			WriteLine("Both parameters are required.");
			WriteLine("Use parameter with (=) the specify the file name.");
			WriteLine("File names must always be enclosed with single quotes (').");
			WriteLine();
			WriteLine("Example:");
			WriteLine();
			WriteLine("  OSoftFormat \\f='c:\\my source code\\filename.cs' \\r='c:\\myrules\\standard.rule'");
			WriteLine();

			WriteLine("Press any key to exit...");
#if DEBUG
			Console.ReadKey();
#endif
		}

		private static void WriteLine()
		{
			Console.WriteLine();
		}

		private static void WriteLine(string message)
		{
			WriteLine(MessageTypes.Information, message);
		}

		private static void WriteLine(MessageTypes type, string message)
		{
			SetConsoleColor(type);
			Console.WriteLine(message);
			ResetConsoleColor();
		}

		private static void WriteFormat(string format, params object[] parameters)
		{
			WriteFormat(MessageTypes.Information, format, parameters);
		}

		private static void WriteFormat(MessageTypes type, string format, params object[] parameters)
		{
			SetConsoleColor(type);
			Console.WriteLine(string.Format(format, parameters));
			ResetConsoleColor();
		}

		private static void Write(string message)
		{
			Write(MessageTypes.Information, message);
		}

		private static void Write(MessageTypes type, string message)
		{
			SetConsoleColor(type);
			Console.Write(message);
			ResetConsoleColor();
		}

		private static void ResetConsoleColor()
		{
			Console.ForegroundColor = ConsoleColor.White;
		}

		private static void SetConsoleColor(MessageTypes type)
		{
			switch (type)
			{
				case MessageTypes.Information:
					Console.ForegroundColor = ConsoleColor.White;
					break;
				case MessageTypes.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case MessageTypes.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				default:
					break;
			}
		}
	}
}
