using ProcessSourceFiles.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace ProcessSourceFiles
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			ProcessingParameters = new ProcessParameters();
			ApplicationName = "Process Source Files";
		}
		public static string ApplicationName { get; set; }
		public static ProcessParameters ProcessingParameters { get; set; }
	}
}
