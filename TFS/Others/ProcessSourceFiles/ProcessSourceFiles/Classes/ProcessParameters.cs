using GregOsborne.Application;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ProcessSourceFiles.Classes
{
	public class ProcessParameters
	{
		public bool AddSpaceBeforeClass { get { return Settings.GetValue<bool>(App.ApplicationName, "Settings\\Before Spacing", "AddSpaceBeforeClass", true); } set { Settings.SetValue<bool>(App.ApplicationName, "Settings\\Before Spacing", "AddSpaceBeforeClass", value); } }
		public bool AddSpaceBeforeNameSpace { get { return Settings.GetValue<bool>(App.ApplicationName, "Settings\\Before Spacing", "AddSpaceBeforeNameSpace", true); } set { Settings.SetValue<bool>(App.ApplicationName, "Settings\\Before Spacing", "AddSpaceBeforeNameSpace", value); } }
		public List<ProcessFile> Files { get; set; }
		public bool RemoveAllBlankLines { get { return Settings.GetValue<bool>(App.ApplicationName, "Settings", "RemoveAllBlankLines", true); } set { Settings.SetValue<bool>(App.ApplicationName, "Settings", "RemoveAllBlankLines", value); } }
		public bool RemoveConsecutiveBlankLines { get { return Settings.GetValue<bool>(App.ApplicationName, "Settings", "RemoveConsecutiveBlankLines", true); } set { Settings.SetValue<bool>(App.ApplicationName, "Settings", "RemoveConsecutiveBlankLines", value); } }
		public bool RemoveFullLineComments { get { return Settings.GetValue<bool>(App.ApplicationName, "Settings", "RemoveFullLineComments", true); } set { Settings.SetValue<bool>(App.ApplicationName, "Settings", "RemoveFullLineComments", value); } }
		public bool RemoveHtmlComments { get { return Settings.GetValue<bool>(App.ApplicationName, "Settings", "RemoveHtmlComments", true); } set { Settings.SetValue<bool>(App.ApplicationName, "Settings", "RemoveHtmlComments", value); } }
		public bool RemoveRegions { get { return Settings.GetValue<bool>(App.ApplicationName, "Settings", "RemoveRegions", true); } set { Settings.SetValue<bool>(App.ApplicationName, "Settings", "RemoveRegions", value); } }
		public ProcessSourceFiles.Classes.ProcessFile.UsingPositions UsingPosition { get { return Settings.GetValue<ProcessSourceFiles.Classes.ProcessFile.UsingPositions>(App.ApplicationName, "Settings", "UsingPosition", ProcessSourceFiles.Classes.ProcessFile.UsingPositions.OutsideNamespace); } set { Settings.SetValue<ProcessSourceFiles.Classes.ProcessFile.UsingPositions>(App.ApplicationName, "Settings", "UsingPosition", value); } }
	}
}
