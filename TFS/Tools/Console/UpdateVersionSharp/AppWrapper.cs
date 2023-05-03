using System;
using System.Configuration;
using ConsoleUtilities;
using GregOsborne.Application;

namespace UpdateVersionSharp {

	public class AppWrapper : ConsoleApplication {

		public AppWrapper(string[] args, string title, string appName, Version version, ApplicationSettingsBase settings, string logDirectory, WriteStatusHandler writeHandler, string installerDirectory, string installerBaseName)
			: base(args, title, logDirectory, appName, version, settings, logDirectory, writeHandler, installerDirectory, installerBaseName) {
		}

		public override void DisplayHelp(string[] args) {
			this.WriteMessage("Command line parameters");
			this.WriteMessage("  -p  Project file name with extension");
			this.WriteMessage("  -h  Display help");
			this.WriteMessage("  -?  ");
			this.WriteMessage();
			this.WriteMessage("Example");
			this.WriteMessage($"  UpdateVersionSharp -p=\"c:\\somepath\\someproject.csproj\"");
			this.WriteMessage();
			this.WriteMessage("  Put parentheses around paths and project names to ensure");
			this.WriteMessage("  any spaces in those items are handled correctly");
			this.WriteMessage();
			this.WriteMessageStay("Press any key to continue...");
			Console.ReadKey();
			Environment.Exit(0);
		}

		public override void Run() => base.Run();
	}
}