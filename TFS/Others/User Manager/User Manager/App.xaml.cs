// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-15-2015
//
// Last Modified By : Greg
// Last Modified On : 07-14-2015
// ***********************************************************************
// <copyright file="App.xaml.cs" company="Statistics & Controls, Inc.">
//     Copyright ©  2015 Statistics & Controls, Inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Shell;
using Ookii.Dialogs.Wpf;
using SNC.Installation.Management;
using User_Manager.Classes;

namespace User_Manager
{
	public partial class App : System.Windows.Application, ISingleInstanceApp
	{
		#region Public Fields
		public static readonly string DefaultInstaller = @"User Manager.ori";
		public static readonly string DefaultInstallFolder = @"\\192.168.0.100\Engineering\Projects\Official Release\OptiRamp Application Packages\User Manager";
		#endregion

		#region Private Fields
		private const int MINIMUM_SPLASH_TIME = 2500;
		private const int SPLASH_FADE_TIME = 200;
		private const string Unique = "OptiRamp_User_Manager";
		#endregion

		#region Public Constructors

		public App()
		{
			WriteEventMessage("Start");
			this.DispatcherUnhandledException += App_DispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			this.Exit += App_Exit;
		}

		#endregion

		#region Public Properties
		public static bool AppRequiresUpdate
		{
			get
			{
				//ISNCInstallationManager installManager = new InstallationManager(ApplicationSettings.GetValue<string>("Application", "RemoteInstallFolder", DefaultInstallFolder), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp"), Version.Parse("2015.6.30.1"), ApplicationSettings.GetValue<string>("Application", "RemoteInstallerFileName", DefaultInstaller));
				ISNCInstallationManager installManager = new InstallationManager(
					ApplicationSettings.GetValue<string>("Application", "RemoteInstallFolder", DefaultInstallFolder),
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp"),
					typeof(MainWindow).Assembly.GetName().Version,
					ApplicationSettings.GetValue<string>("Application", "RemoteInstallerFileName", DefaultInstaller));
				return installManager.RequiresUpdate;
			}
		}

		public static string LogFolder { get; private set; }
		#endregion

		#region Public Methods

		public static void HandleException(Exception ex, bool shutDownApp)
		{
			WriteEventMessage(ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
			var td = new TaskDialog
			{
				AllowDialogCancellation = true,
				ButtonStyle = TaskDialogButtonStyle.Standard,
				CenterParent = true,
				MainIcon = TaskDialogIcon.Error,
				MainInstruction = ex.Message,
				ExpandedInformation = ex.StackTrace,
				MinimizeBox = false,
				WindowTitle = "Application exception"
			};
			var okButton = new TaskDialogButton(ButtonType.Ok);
			td.Buttons.Add(okButton);
			var result = td.ShowDialog(App.Current == null ? null : App.Current.MainWindow);
			if (shutDownApp)
				Environment.Exit(0);
		}

		public static void InstallNewApplicationVersion()
		{
			try
			{
				if (!AppRequiresUpdate)
					return;
				ISNCInstallationManager installManager = new InstallationManager(
					ApplicationSettings.GetValue<string>("Application", "RemoteInstallFolder", DefaultInstallFolder),
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp"),
					typeof(MainWindow).Assembly.GetName().Version,
					ApplicationSettings.GetValue<string>("Application", "RemoteInstallerFileName", DefaultInstaller));

				App.WriteEventMessage(string.Format("Application version {0} exists", installManager.CurrentVersion));
				var td = new TaskDialog
				{
					AllowDialogCancellation = true,
					ButtonStyle = TaskDialogButtonStyle.Standard,
					CenterParent = true,
					MainIcon = TaskDialogIcon.Shield,
					MainInstruction = string.Format("Version {0} of this application is available for installation. Would you like to install the new version?", installManager.InstallVersion),
					ExpandedInformation = string.Format("Current version ({0})", installManager.CurrentVersion),
					MinimizeBox = false,
					WindowTitle = "Application update available"
				};
				var yesButton = new TaskDialogButton(ButtonType.Yes);
				var noButton = new TaskDialogButton(ButtonType.No);
				td.Buttons.Add(yesButton);
				td.Buttons.Add(noButton);
				var result = td.ShowDialog(App.Current.MainWindow);
				if (result == yesButton)
				{
					App.WriteEventMessage("Installing new version");
					installManager.BeginInstallation(true);
					return;
				}
			}
			catch (Exception ex)
			{
				App.HandleException(ex, false);
			}
		}

		[STAThread]
		public static void Main()
		{
			if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
			{
				var application = new App();
				application.InitializeComponent();
				application.Run();
				// Allow single instance code to perform cleanup operations
				SingleInstance<App>.Cleanup();
			}
			else
			{
				var td = new TaskDialog
				{
					AllowDialogCancellation = true,
					ButtonStyle = TaskDialogButtonStyle.Standard,
					CenterParent = true,
					MainIcon = TaskDialogIcon.Error,
					MainInstruction = "There is already an instance of this application running.",
					ExpandedInformation = "Please switch to the running application instance. If you " +
						"don't see this application in your taskbar, start Task Manager and close " +
						"all running instances of User Manager.exe, then attempt application restart.",
					MinimizeBox = false,
					WindowTitle = "Single instance allowed"
				};
				var okButton = new TaskDialogButton(ButtonType.Ok);
				td.Buttons.Add(okButton);
				var result = td.ShowDialog(App.Current == null ? null : App.Current.MainWindow);
			}
		}

		public static void WriteEventMessage(string message)
		{
			WriteEventMessage(message, EventLogEntryType.Information);
		}

		public static void WriteEventMessage(string message, EventLogEntryType type)
		{
			LogFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Logs");
			if (!Directory.Exists(LogFolder))
				Directory.CreateDirectory(LogFolder);
			var logFile = Path.Combine(LogFolder, string.Format("{0}.log", DateTime.Now.ToString("yyyy-MM-dd")));
			var firstline = true;
			using (var sr = new StringReader(message))
			{
				using (var fs = new FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.None))
				using (var sw = new StreamWriter(fs))
				{
					while (sr.Peek() != -1)
					{
						if (firstline)
						{
							sw.Write(DateTime.Now.ToString().PadRight(30));
							sw.Write(type.ToString().PadRight(16));
						}
						else
							sw.Write(new string(' ', 46));
						sw.WriteLine(sr.ReadLine());
						firstline = false;
					}
				}
			}
		}

		public bool SignalExternalCommandLineArgs(IList<string> args)
		{
			return true;
		}

		#endregion

		#region Protected Methods

		protected override void OnStartup(StartupEventArgs e)
		{
			if (!ApplicationSettings.GetValue<bool>("Application", "ManuallyInstallUpdates", true))
				InstallNewApplicationVersion();

			SplashScreen splash = new SplashScreen("images/UserManagerSplash.png");
			splash.Show(false, true);

			Stopwatch timer = new Stopwatch();
			timer.Start();

			base.OnStartup(e);
			MainWindow = new MainWindow();

			timer.Stop();

			int remainingTimeToShowSplash = MINIMUM_SPLASH_TIME - (int)timer.ElapsedMilliseconds;
			if (remainingTimeToShowSplash > 0)
				System.Threading.Thread.Sleep(remainingTimeToShowSplash);

			splash.Close(TimeSpan.FromMilliseconds(SPLASH_FADE_TIME));

			MainWindow.Show();
		}

		#endregion

		#region Private Methods

		private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			App.HandleException(e.Exception, true);
		}

		private void App_Exit(object sender, ExitEventArgs e)
		{
			WriteEventMessage("Stop");
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			App.HandleException((Exception)e.ExceptionObject, true);
		}

		private Version GetLatestInstallVersion(string folder)
		{
			if (!Directory.Exists(folder))
				return null;
			Version result = new Version();
			var baseDir = new DirectoryInfo(folder);
			foreach (var dir in baseDir.GetDirectories())
			{
				var ver = new Version(dir.Name);
				if (ver > result)
					result = ver;
			}
			return result;
		}

		#endregion
	}
}
