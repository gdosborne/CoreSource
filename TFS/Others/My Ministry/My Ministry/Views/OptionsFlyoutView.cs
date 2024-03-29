namespace MyMinistry.Views
{
	using MyMinistry.Utilities;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Windows.Storage;
	using Windows.UI.Popups;

	public class OptionsFlyoutView : BindableBase
	{
		#region Private Fields

		private MyMinistry.Utilities.Enumerations.CongregationAssignments _Assignment;

		private ObservableCollection<MyMinistry.Utilities.Enumerations.CongregationAssignments> _Assignments = null;

		private DelegateCommand _CancelCommand = null;

		private MyMinistryData _Data;

		private Enumerations.DataLocations _DataLocation;

		private ObservableCollection<Enumerations.DataLocations> _DataLocations;

		private string _FirstName;

		private string _LastName;

		private DelegateCommand _SaveCommand = null;

		private Enumerations.DataLocations originalDataLocation = Enumerations.DataLocations.Local;

		#endregion Private Fields

		#region Public Constructors

		public OptionsFlyoutView()
		{
			DataLocations = new ObservableCollection<Enumerations.DataLocations>
			{
				Enumerations.DataLocations.Local,
				Enumerations.DataLocations.OneDrive
			};
			if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("DataLocation"))
			{
				DataLocation = (Enumerations.DataLocations)Enum.Parse(typeof(Enumerations.DataLocations), ApplicationData.Current.RoamingSettings.Values["DataLocation"].ToString(), true);
				originalDataLocation = DataLocation;
			}
		}

		#endregion Public Constructors

		#region Internal Delegates

		internal delegate void CloseHandler(object sender, EventArgs e);

		#endregion Internal Delegates

		#region Internal Events

		internal event CloseHandler Close;

		#endregion Internal Events

		#region Public Properties

		public MyMinistry.Utilities.Enumerations.CongregationAssignments Assignment {
			get { return _Assignment; }
			set {
				_Assignment = value;
				OnPropertyChanged("Assignment");
			}
		}

		public ObservableCollection<MyMinistry.Utilities.Enumerations.CongregationAssignments> Assignments {
			get { return _Assignments; }
			set {
				_Assignments = value;
				OnPropertyChanged("Assignments");
			}
		}

		public DelegateCommand CancelCommand {
			get {
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}

		public MyMinistryData Data {
			get { return _Data; }
			set {
				_Data = value;
				Assignments = new ObservableCollection<Enumerations.CongregationAssignments>
				{
					Enumerations.CongregationAssignments.Publisher,
					Enumerations.CongregationAssignments.AuxiliaryPioneer,
					Enumerations.CongregationAssignments.RegularPioneer,
					Enumerations.CongregationAssignments.SpecialPioneer,
					Enumerations.CongregationAssignments.Missionary
				};
				if (value != null && value.User != null)
				{
					FirstName = value.User.FirstName;
					LastName = value.User.LastName;
					Assignment = value.User.Assignment;
				}
				OnPropertyChanged("User");
			}
		}

		public Enumerations.DataLocations DataLocation {
			get { return _DataLocation; }
			set {
				_DataLocation = value;
				OnPropertyChanged("DataLoadType");
			}
		}

		public ObservableCollection<Enumerations.DataLocations> DataLocations {
			get { return _DataLocations; }
			set {
				_DataLocations = value;
				OnPropertyChanged("DataLoadTypes");
			}
		}

		public string FirstName {
			get { return _FirstName; }
			set {
				_FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}

		public string LastName {
			get { return _LastName; }
			set {
				_LastName = value;
				OnPropertyChanged("LastName");
			}
		}

		public DelegateCommand SaveCommand {
			get {
				if (_SaveCommand == null)
					_SaveCommand = new DelegateCommand(Save, ValidateSaveState);
				return _SaveCommand as DelegateCommand;
			}
		}

		#endregion Public Properties

		#region Private Methods

		private void Cancel(object state)
		{
			if (Close != null)
				Close(this, EventArgs.Empty);
		}

		private async void Save(object state)
		{
			if (Data != null)
			{
				if (Data.User != null)
				{
					Data.User.Assignment = Assignment;
					Data.User.FirstName = FirstName;
					Data.User.LastName = LastName;
				}
				CommonData.Data = Data;
				if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("DataLocation"))
					ApplicationData.Current.RoamingSettings.Values["DataLocation"] = DataLocation.ToString();
				else
					ApplicationData.Current.RoamingSettings.Values.Add("DataLocation", DataLocation.ToString());
				if (originalDataLocation != DataLocation)
				{
					await new MessageDialog("You must restart the application for the data location change to take affect.", "Data location changed").ShowAsync();
				}
				await MyMinistryData.WriteInfo<MyMinistryData>(Data, CommonData.FolderId, CommonData.DataFile != null ? CommonData.DataFile.FileName : "myministrydata.xml", CommonData.Client);
			}
			if (Close != null)
				Close(this, EventArgs.Empty);
			CommonData.OnDataChanged();
		}

		private bool ValidateCancelState(object state)
		{
			return true;
		}

		private bool ValidateSaveState(object state)
		{
			return true;
		}

		#endregion Private Methods
	}
}
