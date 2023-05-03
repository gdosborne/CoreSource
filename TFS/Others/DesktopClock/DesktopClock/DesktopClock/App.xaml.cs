namespace DesktopClock
{
	using OSoftComponents;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Windows;

	public partial class App : System.Windows.Application
	{
		#region Protected Methods
		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			if (Activity != null)
			{
				Activity.Dispose();
				Activity = null;
			}
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}
		#endregion Protected Methods

		#region Private Methods
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			LogException((Exception)e.ExceptionObject);
			Environment.Exit(999);
		}
		private void LogException(Exception ex)
		{
			var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OSoft");
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);
			var file = string.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd"));
			var fullFileName = Path.Combine(folder, file);
			using (var fs = new FileStream(fullFileName, FileMode.Append, FileAccess.Write, FileShare.None))
			using (var sw = new StreamWriter(fs))
			{
				sw.WriteLine(new String('*', 50));
				sw.WriteLine("Exception details");
				sw.WriteLine(DateTime.Now);
				WriteException(sw, ex, 0);
			}

			var p = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "Explorer.exe",
					Arguments = folder
				}
			};
			p.Start();
		}
		private void WriteException(StreamWriter sw, Exception ex, int indent)
		{
			var indentText = new string(' ', indent * 4);
			sw.WriteLine(indentText + ex.Message);
			if (ex.StackTrace != null && ex.StackTrace.Any())
			{
				var st = new StackTrace(ex, true);
				st.GetFrames().ToList().ForEach(x =>
				{
					sw.WriteLine(indentText + string.Format("at {0} {2} (Line {1})", x.GetFileName(), x.GetFileLineNumber(), x.GetMethod()));
				});
			}
			if (ex.InnerException != null)
				WriteException(sw, ex.InnerException, indent + 1);
		}
		#endregion Private Methods

		#region Public Properties
		public static DiskActivity Activity { get; set; }
		#endregion Public Properties
	}
}
