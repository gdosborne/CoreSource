namespace ProcessSourceFiles
{
	using ProcessSourceFiles.Classes;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	public partial class App : Application
	{
		#region Protected Methods
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			ProcessingParameters = new ProcessParameters();
			ApplicationName = "Process Source Files";
		}
		#endregion Protected Methods

		#region Public Properties
		public static string ApplicationName { get; set; }
		public static ProcessParameters ProcessingParameters { get; set; }
		#endregion Public Properties
	}
}
