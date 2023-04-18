using Ookii.Dialogs.Wpf;
using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Universal.Common;

namespace Territory.Checkout.Data {
	internal class DataItemBase : INotifyPropertyChanged, IDisposable {
		public DataItemBase(long id) {
			ID = id;
			Status = Statuses.Unchanged;
		}

		#region PropertyChanged
		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		protected void InvokePropertyChanged([CallerMemberName] string propertyName = null) =>
			OnPropertyChanged(propertyName);
		#endregion

		#region ID Property
		private long _ID = default;
		public long ID {
			get => _ID;
			set {
				_ID = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region Status Property
		private Statuses _Status = default;
		public Statuses Status {
			get => _Status;
			set {
				_Status = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsLoading Property
		private bool _IsLoading = true;
		public bool IsLoading {
			get => _IsLoading;
			set {
				_IsLoading = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		internal enum Statuses {
			Unchanged,
			Changed,
			NewlyAdded,
			Deleted
		}

		private static string databaseName => "Territory1.accdb";
		private static string userDirectory { get; set; } = @"C:\Users\greg";
		private static string databasePath { get; set; } = @$"{userDirectory}\Dropbox\Territory\{databaseName}";
		private static string connectionString { get; set; } = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}";
		
		private static OleDbConnection _Connection = default;
		protected static OleDbConnection Connection {
			get {
				if (_Connection == null) {

					CheckForDatabase();
					CheckForNewApplication();

					_Connection = new OleDbConnection(connectionString);
					_Connection.Open();
				}
				return _Connection;
			}
		}

		private static void CheckForNewApplication() {
			userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			var dropBoxDirectory = System.IO.Path.Combine(userDirectory, "DropBox");
			if (System.IO.Directory.Exists(dropBoxDirectory)) {
				var installDir = System.IO.Path.Combine(dropBoxDirectory, "Territory", "Application");
				if(System.IO.Directory.Exists(installDir)) {
					var installs = new System.IO.DirectoryInfo(installDir).GetFiles("TCSetup_*.exe");
					if(installs.Any()) {
						var version = default(Version?);
						installs.ForEach(file => {
							var ver = Version.Parse(file.Name.Replace("TCSetup_", string.Empty).Replace(".exe", string.Empty));
							if (version == null) 
								version = ver;
							else
								version = ver > version ? ver : version;
						});
						var currentVersion = Assembly.GetEntryAssembly().GetName().Version;
						if (currentVersion < version) {
							var install = new FileInfo(Path.Combine(installDir, $"TCSetup_{version}.exe"));
							if (install.Exists) {
								var result = App.DisplayYesNoDialog(null, "New application update found!",
									$"A new application installer has been found in the Territory folder.\n\n" +
									$"Would you like to install version {version} of the Territory Checkout Application?",
									"New installation found");
								if(result) {
									var p = new Process {
										StartInfo = new ProcessStartInfo {
											FileName = install.FullName
										}
									};
									p.Start();
									Environment.Exit(0);
								}
							}
						}
					}
				}
			}
		}

		private static void CheckForDatabase() {
			userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			var dropBoxDirectory = System.IO.Path.Combine(userDirectory, "DropBox");
			if (!System.IO.Directory.Exists(dropBoxDirectory)) {
				var dbPath = App.AppSettings.GetValue("Application", "Database.Location", string.Empty);
				if (!string.IsNullOrEmpty(dbPath) && System.IO.File.Exists(dbPath)) {
					databasePath = dbPath;					
					return;
				}
				var result = App.DisplayYesNoDialog(null, "DropBox© folder not found",
					$"This application is searching for your local DropBox© folder. It should have been located " +
					$"at {dropBoxDirectory}, but it was not found. The database for this application is located " +
					$"within DropBox©.\n\nWould you like to search for the file Territory1.accdb yourself?",
					"DropBox© folder not found");
				if (result) {
					var dlg = new VistaOpenFileDialog {
						AddExtension = false,
						CheckFileExists = true,
						CheckPathExists = true,
						FileName = databaseName,
						Filter = "Access databases|*.accdb",
						InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
						Title = "Find application database..."
					};
					var result1 = dlg.ShowDialog();
					if (!result1.HasValue || !result1.Value) Environment.Exit(0);					
					App.AppSettings.AddOrUpdateSetting("Application", "Database.Location", dlg.FileName);
				}
				else {
					Environment.Exit(0);
				}
			}
		}

		private bool hasBeenDisposed;
		protected virtual void Dispose(bool isDisposing) {
			if (!hasBeenDisposed) {
				if (isDisposing) {
					if (_Connection != null) {
						if (_Connection.State == System.Data.ConnectionState.Open) {
							_Connection.Close();
						}
						_Connection.Dispose();
					}
				}
				hasBeenDisposed = true;
			}
		}

		public void Dispose() {
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(isDisposing: true);
			GC.SuppressFinalize(this);
		}

		protected static OleDbParameter GetParameter(string parameterName, object value) =>
			new OleDbParameter(parameterName, value == null ? DBNull.Value : value);

		protected static OleDbParameter GetParameter(string parameterName, object value, OleDbType dataType) =>
			new OleDbParameter(parameterName, dataType) { Value = value == null ? DBNull.Value : value };

		public virtual Task Update() { return null; }
		public virtual Task Delete() { return null; }
	}
}
