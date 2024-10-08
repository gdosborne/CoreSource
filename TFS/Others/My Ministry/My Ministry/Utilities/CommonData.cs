namespace MyMinistry.Utilities
{
	using Microsoft.Live;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Views;
	using Windows.Storage;
	using Windows.UI.Xaml;

	internal static class CommonData
	{
		#region Public Fields

		public static DateTime? MinistrySessionEnd = null;
		public static DateTime? MinistrySessionStart = null;
		public static TimeSpan? MinistrySessionTime = null;
		public static string MinistryTimeDisplay = "0:00:00";

		#endregion Public Fields

		#region Private Fields

		private static TimeActionPanelView MinistryTimeView = null;

		#endregion Private Fields

		#region Public Delegates

		public delegate void ChangeHandler(EventArgs e);

		#endregion Public Delegates

		#region Public Events

		public static event ChangeHandler DataChanged;

		#endregion Public Events

		#region Public Properties

		public static LiveConnectClient Client { get; set; }
		public static MyMinistryContact ContactToEdit { get; set; }
		public static MyMinistryData Data { get; set; }
		public static OneDriveFile DataFile { get; set; }
		public static string FolderId { get; set; }

		public static bool IsCompactButtons {
			get {
				return !(bool)ApplicationData.Current.RoamingSettings.Values["ShowAppBarLabels"];
			}
			set {
				ApplicationData.Current.RoamingSettings.Values["ShowAppBarLabels"] = !value;
				OnDataChanged();
			}
		}

		public static StorageFile LocalFile { get; set; }
		public static DispatcherTimer MinistrySessionTimer { get; set; }

		#endregion Public Properties

		#region Public Methods

		public static void OnDataChanged()
		{
			if (DataChanged != null)
				DataChanged(EventArgs.Empty);
		}

		public static void StartMinistryTime(TimeActionPanelView view)
		{
			MinistryTimeView = view;
			if (MinistrySessionTimer == null)
			{
				MinistrySessionTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
				MinistrySessionTimer.Tick += MinistrySessionTimer_Tick;
			}
			MinistrySessionTimer.Start();
			MinistryTimeView.StartDate = DateTime.Now;
		}

		public static void StopMinistryTime()
		{
			MinistrySessionTimer.Stop();
			MinistryTimeView.EndDate = DateTime.Now;
			MinistryTimeView.CurrentTimeSpan = MinistrySessionEnd.Value.Subtract(MinistrySessionStart.Value);
			//record time
			MinistryTimeView.StartDate = null;
			MinistryTimeView.EndDate = null;
			MinistryTimeView = null;
		}

		#endregion Public Methods

		#region Private Methods

		private static void MinistrySessionTimer_Tick(object sender, object e)
		{
			if (MinistrySessionStart.HasValue)
				MinistryTimeView.CurrentTimeSpan = DateTime.Now.Subtract(MinistrySessionStart.Value);
		}

		#endregion Private Methods
	}
}
