namespace SoundDesk
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Text;
	//using GregOsborne.Application.Exception;
	using GregOsborne.Application.IO;
	using GregOsborne.Dialog;
	using SysIO=System.IO;
	using System.Text;

	public partial class App : Application
	{
		#region Public Constructors
		public App()
		{
			if (SoundDesk.Properties.Settings.Default.RequiresUpdate)
			{
				SoundDesk.Properties.Settings.Default.Upgrade();
				SoundDesk.Properties.Settings.Default.RequiresUpdate = false;
				SoundDesk.Properties.Settings.Default.Save();
			}
			this.DispatcherUnhandledException += App_DispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}
		private static void WriteUnhandledException(Exception ex, string exceptionFile)
		{
			//var sb1 = ex.ToStringRecurse();
			//var sb2 = System.Diagnostics.Process.GetCurrentProcess().ProcessData();
			//using (var fs = File.OpenFile(exceptionFile, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
			//using (var sw = new System.IO.StreamWriter(fs))
			//{
			//	sw.WriteLine(sb1.ToString());
			//	sw.WriteLine(sb2.ToString());
			//}
		}
		private static void WriteExceptionText(string message, string exceptionFile)
		{
			//var sb1 = new StringBuilder(message); ;
			//var sb2 = System.Diagnostics.Process.GetCurrentProcess().ProcessData();
			//using (var fs = File.OpenFile(exceptionFile, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
			//using (var sw = new System.IO.StreamWriter(fs))
			//{
			//	sw.WriteLine(sb1.ToString());
			//	sw.WriteLine(sb2.ToString());
			//}
		}
		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var ex = e.ExceptionObject.As<Exception>();
			App.HandleException(ex, true);
		}
		public static void HandleException(Exception ex, bool shutdown = false)
		{
			var exceptionFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Exceptions", "Error.txt");
			if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(exceptionFile)))
				System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(exceptionFile));
			WriteUnhandledException(ex, exceptionFile);
			if (shutdown)
			{
				var p = new System.Diagnostics.Process
				{
					StartInfo = new System.Diagnostics.ProcessStartInfo
					{
						FileName = exceptionFile
					}
				};
				p.Start();
				Application.Current.Shutdown(0);
			}
			else
			{
				//DisplayErrorDialog(ex.ToStringRecurse().ToString(), exceptionFile);
			}
		}
		public static void DisplayErrorDialog(string format, params object[] p)
		{
			DisplayErrorDialog(string.Format(format, p));
		}
		public static void DisplayErrorDialog(string message)
		{
			var exceptionFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Exceptions", "Error.txt");
			if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(exceptionFile)))
				System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(exceptionFile));
			WriteExceptionText(message, exceptionFile);
			var td = new TaskDialog
			{
				AllowClose = false,
				Width = 600,
				Image = ImagesTypes.Error,
				MessageText = message,
				AdditionalInformation = string.Format("This information is also contained in \"{0}\".", exceptionFile),
				Title = "Exception"
			};
			td.AddButtons(ButtonTypes.OK);
			if (Application.Current.MainWindow != null)
				td.ShowDialog(Application.Current.MainWindow);
			else
				td.Show();
		}
		void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			CurrentDomain_UnhandledException(sender, new UnhandledExceptionEventArgs(e.Exception, e.Dispatcher.HasShutdownStarted));
		}

		#endregion Public Constructors

		#region Public Methods
		public static UpdateService.UpdateServiceClient GetClient()
		{
			return new UpdateService.UpdateServiceClient("BasicHttpBinding_IUpdateService", SoundDesk.Properties.Settings.Default.UpdateUrl);
		}
		#endregion Public Methods

		#region Protected Methods
		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
		}
		#endregion Protected Methods

		#region Public Fields
		public static readonly string ApplicationName = "SoundDesk";
		public static bool InstallNewVersionOnExit = false;
		#endregion Public Fields
	}
}
