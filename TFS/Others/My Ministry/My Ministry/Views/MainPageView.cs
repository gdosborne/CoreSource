namespace MyMinistry.Views
{
	using Microsoft.Live;
	using MyMinistry.Utilities;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using Windows.Storage;
	using Windows.UI.Core;
	using Windows.UI.Popups;
	using Windows.UI.Xaml;

	public class MainPageView : BindableBase
	{
		#region Private Fields

		private Visibility _AddUserVisibility;
		private ObservableCollection<MyMinistry.Utilities.Enumerations.CongregationAssignments> _Assignments;
		private DelegateCommand _CancelCommand = null;
		private ObservableCollection<string> _Cities;
		private LiveConnectClient _Client;
		private DelegateCommand _ContactsCommand = null;
		private string _ContactsTooltip;
		private Visibility _ContactsVisibility;
		private MyMinistryData _Data;
		private OneDriveFile _DataFile;
		private ObservableCollection<OneDriveFile> _Files;
		private string _FirstName;
		private string _FolderName;
		private DelegateCommand _HoursCommand = null;
		private string _HoursTooltip;
		private string _LastName;
		private StorageFolder _LocalCacheFolder = null;
		private ApplicationDataContainer _LocalSettings = null;
		private string _MinistryFileName;
		private DelegateCommand _PlacementsCommand = null;
		private Visibility _ProgressVisibility;
		private StorageFolder _RoamingFolder = null;
		private ApplicationDataContainer _RoamingSettings = null;
		private DelegateCommand _SaveCommand = null;
		private DelegateCommand _ScheduleCommand = null;
		private MyMinistry.Utilities.Enumerations.CongregationAssignments _SelectedAssignment;
		private MyMinistryContact _SelectedContact;
		private DelegateCommand _TerritoriesCommand = null;
		private Visibility _TimeActionsPanelVisibility;
		private DelegateCommand _VideosCommand = null;

		#endregion Private Fields

		#region Public Constructors

		public MainPageView()
		{
			AddUserVisibility = Visibility.Collapsed;
			ContactsVisibility = Visibility.Collapsed;
			ProgressVisibility = Visibility.Visible;

			Assignments = new ObservableCollection<Enumerations.CongregationAssignments>
			{
				Enumerations.CongregationAssignments.Publisher,
				Enumerations.CongregationAssignments.AuxiliaryPioneer,
				Enumerations.CongregationAssignments.RegularPioneer,
				Enumerations.CongregationAssignments.SpecialPioneer,
				Enumerations.CongregationAssignments.Missionary
			};
			SelectedAssignment = Enumerations.CongregationAssignments.Publisher;

			LocalSettings = ApplicationData.Current.LocalSettings;
			RoamingSettings = ApplicationData.Current.RoamingSettings;
			RoamingFolder = ApplicationData.Current.RoamingFolder;
			LocalFolder = ApplicationData.Current.LocalFolder;

			Files = new ObservableCollection<OneDriveFile>();

			FolderName = "My Ministry";
			if (CommonData.Data == null)
				GetSignIn();
			else
			{
				Data = CommonData.Data;
				FolderId = CommonData.FolderId;
				DataFile = CommonData.DataFile;
				Client = CommonData.Client;
				Cities = new ObservableCollection<string>(Data.Contacts.Select(x => x.City).Distinct().OrderBy(x => x));
			}
			CommonData.DataChanged += CommonData_DataChanged;
			RoamingSettings.Values["FolderId"] = FolderId;

			if (RoamingSettings.Values.ContainsKey("ShowAppBarLabels"))
				CommonData.IsCompactButtons = !(bool)RoamingSettings.Values["ShowAppBarLabels"];
			else
				CommonData.IsCompactButtons = false;
			TimeActionsPanelVisibility = GetQuickPanelVisibility("TimePanelVisibility", Visibility.Visible);
		}

		#endregion Public Constructors

		#region Public Delegates

		public delegate void NavigatationHandler(object sender, NavigationEventArgs e);

		#endregion Public Delegates

		#region Public Events

		public event NavigatationHandler Navigate;

		#endregion Public Events

		#region Public Properties

		public Visibility AddUserVisibility {
			get { return _AddUserVisibility; }
			set {
				_AddUserVisibility = value;
				UpdateInterface();
				OnPropertyChanged("AddUserVisibility");
			}
		}

		public ObservableCollection<MyMinistry.Utilities.Enumerations.CongregationAssignments> Assignments {
			get { return _Assignments; }
			set {
				_Assignments = value;
				UpdateInterface();
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

		public ObservableCollection<string> Cities {
			get { return _Cities; }
			set {
				_Cities = value;
				UpdateInterface();
				OnPropertyChanged("Cities");
			}
		}

		public LiveConnectClient Client {
			get { return _Client; }
			set {
				_Client = value;
				CommonData.Client = value;
				UpdateInterface();
				OnPropertyChanged("Client");
			}
		}

		public DelegateCommand ContactsCommand {
			get {
				if (_ContactsCommand == null)
					_ContactsCommand = new DelegateCommand(Contacts, ValidateContactsState);
				return _ContactsCommand as DelegateCommand;
			}
		}

		public string ContactsTooltip {
			get { return _ContactsTooltip; }
			set {
				_ContactsTooltip = value;
				OnPropertyChanged("ContactsTooltip");
			}
		}

		public Visibility ContactsVisibility {
			get { return _ContactsVisibility; }
			set {
				_ContactsVisibility = value;
				UpdateInterface();
				OnPropertyChanged("ContactsVisibility");
			}
		}

		public MyMinistryData Data {
			get { return _Data; }
			set {
				_Data = value;
				if (value != null)
				{
					CommonData.Data = value;
					if (Data.User != null)
					{
						FirstName = Data.User.FirstName;
						LastName = Data.User.LastName;
						SelectedAssignment = Data.User.Assignment;
					}
				}
				UpdateInterface();
				OnPropertyChanged("Data");
			}
		}

		public OneDriveFile DataFile {
			get { return _DataFile; }
			set {
				_DataFile = value;
				CommonData.DataFile = value;
				UpdateInterface();
				OnPropertyChanged("DataFile");
			}
		}

		public Enumerations.DataLocations DataLocation {
			get {
				return RoamingSettings.Values.ContainsKey("DataLocation")
				  ? (Enumerations.DataLocations)Enum.Parse(typeof(Enumerations.DataLocations), RoamingSettings.Values["DataLocation"].ToString(), true)
				  : Enumerations.DataLocations.Local;
			}
			set {
				if (RoamingSettings.Values.ContainsKey("DataLocation"))
					RoamingSettings.Values["DataLocation"] = value.ToString();
				else
					RoamingSettings.Values.Add("DataLocation", value.ToString());
				UpdateInterface();
				OnPropertyChanged("DataLocation");
			}
		}

		public ObservableCollection<OneDriveFile> Files {
			get { return _Files; }
			set {
				_Files = value;
				UpdateInterface();
				OnPropertyChanged("Files");
			}
		}

		public string FirstName {
			get { return _FirstName; }
			set {
				_FirstName = value;
				UpdateInterface();
				OnPropertyChanged("FirstName");
			}
		}

		public string FolderId {
			get {
				return RoamingSettings.Values.ContainsKey("FolderId")
				  ? (string)RoamingSettings.Values["FolderId"]
				  : string.Empty;
			}
			set {
				RoamingSettings.Values["FolderId"] = value;
				CommonData.FolderId = value;
				UpdateInterface();
				OnPropertyChanged("FolderId");
			}
		}

		public string FolderName {
			get { return _FolderName; }
			set {
				_FolderName = value;
				UpdateInterface();
				OnPropertyChanged("FolderName");
			}
		}

		public DelegateCommand HoursCommand {
			get {
				if (_HoursCommand == null)
					_HoursCommand = new DelegateCommand(Hours, ValidateHoursState);
				return _HoursCommand as DelegateCommand;
			}
		}

		public string HoursTooltip {
			get { return _HoursTooltip; }
			set {
				_HoursTooltip = value;
				OnPropertyChanged("HoursTooltip");
			}
		}

		public bool IsCompactButtons {
			get { return CommonData.IsCompactButtons; }
			set {
				CommonData.IsCompactButtons = value;
				UpdateInterface();
				OnPropertyChanged("IsCompactButtons");
			}
		}

		public bool IsSignedIn {
			get { return LocalSettings.Values.ContainsKey("IsSignedIn") && (bool)LocalSettings.Values["IsSignedIn"]; }
			set {
				LocalSettings.Values["IsSignedIn"] = value;
				UpdateInterface();
				OnPropertyChanged("IsSignedIn");
			}
		}

		public string LastName {
			get { return _LastName; }
			set {
				_LastName = value;
				UpdateInterface();
				OnPropertyChanged("LastName");
			}
		}

		public StorageFolder LocalFolder {
			get { return _LocalCacheFolder; }
			set {
				_LocalCacheFolder = value;
				UpdateInterface();
				OnPropertyChanged("LocalCacheFolder");
			}
		}

		public ApplicationDataContainer LocalSettings {
			get { return _LocalSettings; }
			set {
				_LocalSettings = value;
				UpdateInterface();
				OnPropertyChanged("LocalSettings");
			}
		}

		public string MinistryFileName {
			get { return _MinistryFileName; }
			set {
				_MinistryFileName = value;
				OnPropertyChanged("MinistryFileName");
			}
		}

		public DelegateCommand PlacementsCommand {
			get {
				if (_PlacementsCommand == null)
					_PlacementsCommand = new DelegateCommand(Placements, ValidatePlacementsState);
				return _PlacementsCommand as DelegateCommand;
			}
		}

		public Visibility ProgressVisibility {
			get { return _ProgressVisibility; }
			set {
				_ProgressVisibility = value;
				OnPropertyChanged("ProgressVisibility");
			}
		}

		public StorageFolder RoamingFolder {
			get { return _RoamingFolder; }
			set {
				_RoamingFolder = value;
				UpdateInterface();
				OnPropertyChanged("RoamingFolder");
			}
		}

		public ApplicationDataContainer RoamingSettings {
			get { return _RoamingSettings; }
			set {
				_RoamingSettings = value;
				UpdateInterface();
				OnPropertyChanged("RoamingSettings");
			}
		}

		public DelegateCommand SaveCommand {
			get {
				if (_SaveCommand == null)
					_SaveCommand = new DelegateCommand(Save, ValidateSaveState);
				return _SaveCommand as DelegateCommand;
			}
		}

		public DelegateCommand ScheduleCommand {
			get {
				if (_ScheduleCommand == null)
					_ScheduleCommand = new DelegateCommand(Schedule, ValidateScheduleState);
				return _ScheduleCommand as DelegateCommand;
			}
		}

		public MyMinistry.Utilities.Enumerations.CongregationAssignments SelectedAssignment {
			get { return _SelectedAssignment; }
			set {
				_SelectedAssignment = value;
				UpdateInterface();
				OnPropertyChanged("SelectedAssignment");
			}
		}

		public MyMinistryContact SelectedContact {
			get { return _SelectedContact; }
			set {
				_SelectedContact = value;
				UpdateInterface();
				OnPropertyChanged("SelectedContact");
			}
		}

		public DelegateCommand TerritoriesCommand {
			get {
				if (_TerritoriesCommand == null)
					_TerritoriesCommand = new DelegateCommand(Territories, ValidateTerritoriesState);
				return _TerritoriesCommand as DelegateCommand;
			}
		}

		public Visibility TimeActionsPanelVisibility {
			get { return _TimeActionsPanelVisibility; }
			set {
				_TimeActionsPanelVisibility = value;
				UpdateInterface();
				OnPropertyChanged("TimeActionsPanelVisibility");
			}
		}

		public DelegateCommand VideosCommand {
			get {
				if (_VideosCommand == null)
					_VideosCommand = new DelegateCommand(Videos, ValidateVideosState);
				return _VideosCommand as DelegateCommand;
			}
		}

		#endregion Public Properties

		#region Public Methods

		public void UpdateInterface()
		{
			SaveCommand.RaiseCanExecuteChanged();
			CancelCommand.RaiseCanExecuteChanged();
			ContactsCommand.RaiseCanExecuteChanged();
			ProgressVisibility = Data != null ? Visibility.Collapsed : Visibility.Visible;
		}

		#endregion Public Methods

		#region Private Methods

		private void Cancel(object state)
		{
			FirstName = string.Empty;
			LastName = string.Empty;
			SelectedAssignment = Enumerations.CongregationAssignments.Publisher;
			AddUserVisibility = Visibility.Collapsed;
			ContactsVisibility = Visibility.Collapsed;
		}

		private void CommonData_DataChanged(EventArgs e)
		{
			if (Data != null && Data.User != null)
			{
				FirstName = Data.User.FirstName;
				LastName = Data.User.LastName;
				SelectedAssignment = Data.User.Assignment;
			}
			ContactsTooltip = IsCompactButtons ? "Show contacts" : null;
			HoursTooltip = IsCompactButtons ? "Show hours" : null;

			UpdateInterface();
			OnPropertyChanged("Data");
		}

		private void Contacts(object state)
		{
			OnExecuteUIAction("ShowContacts", null);
		}

		private async Task FetchFolderId()
		{
			try
			{
				LiveOperationResult liveResult = await Client.GetAsync("me/skydrive/files");
				dynamic result = liveResult.Result;
				FolderId = string.Empty;
				foreach (dynamic file in result.data)
				{
					if (file.type == "folder" && file.name == FolderName)
					{
						FolderId = file.id;
						break;
					}
				}
				if (string.IsNullOrEmpty(FolderId))
				{
					var folderData = new Dictionary<string, object>();
					folderData.Add("name", FolderName);
					LiveOperationResult operationResult = await Client.PostAsync("me/skydrive", folderData);
					dynamic r = operationResult.Result;
					foreach (dynamic item in r)
					{
						if (item.Key == "id")
						{
							FolderId = item.Value;
							break;
						}
					}
				}
			}
			catch (LiveAuthException ex)
			{
				Debug.WriteLine("Exception during sign-in: {0}", ex.Message);
			}
			catch (Exception ex)
			{
				// Get the code monkey's attention.
				//Debugger.Break();
			}
		}

		private async Task GetDataFile()
		{
			try
			{
				switch (DataLocation)
				{
					case Enumerations.DataLocations.Local:
						StorageFile file = null;
						var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
						openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
						openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
						openPicker.FileTypeFilter.Add(".xml");

						file = await openPicker.PickSingleFileAsync();
						if (file == null)
						{
							await new MessageDialog("You did not select a file to open. Type in the new file name in the next save file dialog.", "Create new data file").ShowAsync();
							Data = new MyMinistryData { CreationDate = DateTime.Now.Date };
							var savePicker = new Windows.Storage.Pickers.FileSavePicker();
							savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
							savePicker.FileTypeChoices.Add("Xml Files", new List<string> { ".xml" });
							file = await savePicker.PickSaveFileAsync();
							if (file == null)
							{
								await new MessageDialog("You must select a file to open or a new file to save. Please restart application.", "File missing").ShowAsync();
								Application.Current.Exit();
								return;
							}
							CommonData.LocalFile = file;
							MinistryFileName = file.Path;
							await MyMinistryData.WriteInfo(Data, FolderId, file);
							AddUserVisibility = Visibility.Visible;
						}
						else
						{
							CommonData.LocalFile = file;
							MinistryFileName = file.Path;
							var stream = await file.OpenStreamForReadAsync();
							using (StreamReader reader = new StreamReader(stream))
							{
								Data = await MyMinistryData.ReadInfo(reader.ReadToEnd());
							}
							if (Data.User == null || string.IsNullOrEmpty(Data.User.FirstName) || string.IsNullOrEmpty(Data.User.LastName))
								AddUserVisibility = Visibility.Visible;
						}
						break;

					case Enumerations.DataLocations.OneDrive:
						LiveOperationResult lor = await Client.GetAsync(this.FolderId + @"/files");
						dynamic result = lor.Result;
						Files.Clear();
						foreach (dynamic file1 in result.data)
						{
							if (file1.type == "file")
							{
								string name = file1.name;
								string id = file1.id;
								Files.Add(new OneDriveFile { FileName = name, FileId = id });
							}
						}
						MinistryFileName = "myministrydata.xml";
						if (!Files.Any(x => x.FileName.Equals(MinistryFileName, StringComparison.OrdinalIgnoreCase)))
						{
							Data = new MyMinistryData { CreationDate = DateTime.Now.Date };
							AddUserVisibility = Visibility.Visible;
						}
						else
						{
							DataFile = Files.First(x => x.FileName.Equals(MinistryFileName, StringComparison.OrdinalIgnoreCase));
							LiveDownloadOperationResult ldor = await Client.BackgroundDownloadAsync(DataFile.FileId + @"/content");
							var stream = ldor.Stream.AsStreamForRead(0);
							using (StreamReader reader = new StreamReader(stream))
							{
								Data = await MyMinistryData.ReadInfo(reader.ReadToEnd());
							}
						}
						break;
				}

				Cities = new ObservableCollection<string>(Data.Contacts.Select(x => x.City).Distinct().OrderBy(x => x));
				UpdateInterface();
			}
			catch (LiveAuthException ex)
			{
				Debug.WriteLine("Exception during sign-in: {0}", ex.Message);
			}
			catch (Exception ex)
			{
				MinistryFileName = null;
			}
			finally
			{
				ProgressVisibility = Visibility.Collapsed;
			}
		}

		private Visibility GetQuickPanelVisibility(string panelName, Visibility defaultValue)
		{
			if (RoamingSettings.Values.ContainsKey(panelName))
				return (Visibility)Enum.Parse(typeof(Visibility), (string)RoamingSettings.Values[panelName]);
			else
				return defaultValue;
		}

		private async void GetSignIn()
		{
			await SignIn();
			await GetDataFile();
		}

		private void Hours(object state)
		{
		}

		private void Placements(object state)
		{
		}

		private async void Save(object state)
		{
			if (Data.User == null || (string.IsNullOrEmpty(Data.User.FirstName) || string.IsNullOrEmpty(Data.User.LastName)))
				Data.User = new MyMinistryUser { LastName = LastName, FirstName = FirstName, Assignment = SelectedAssignment };
			if (DataLocation == Enumerations.DataLocations.Local)
			{
				if (CommonData.LocalFile != null)
					await MyMinistryData.WriteInfo(Data, FolderId, CommonData.LocalFile);
			}
			else
			{
				await MyMinistryData.WriteInfo<MyMinistryData>(Data, FolderId, DataFile != null ? DataFile.FileName : MinistryFileName, Client);
			}
			AddUserVisibility = Visibility.Collapsed;
		}

		private void Schedule(object state)
		{
		}

		private async Task SignIn()
		{
			try
			{
				LiveAuthClient auth = new LiveAuthClient();
				var loginResult = await auth.LoginAsync(new string[] { "wl.signin", "wl.skydrive", "wl.skydrive_update" });
				Client = new LiveConnectClient(loginResult.Session);
				IsSignedIn = (loginResult.Status == LiveConnectSessionStatus.Connected);
				await FetchFolderId();
			}
			catch (LiveAuthException ex)
			{
				Debug.WriteLine("Exception during sign-in: {0}", ex.Message);
			}
			catch (Exception ex)
			{
				// Get the code monkey's attention.
				Debugger.Break();
			}
		}

		private void Territories(object state)
		{
		}

		private bool ValidateCancelState(object state)
		{
			return true;
		}

		private bool ValidateContactsState(object state)
		{
			return Data != null;
		}

		private bool ValidateHoursState(object state)
		{
			return false;
		}

		private bool ValidateOptionsState(object state)
		{
			return true;
		}

		private bool ValidatePlacementsState(object state)
		{
			return false;
		}

		private bool ValidateSaveState(object state)
		{
			return !string.IsNullOrEmpty(LastName)
				&& !string.IsNullOrEmpty(FirstName);
		}

		private bool ValidateScheduleState(object state)
		{
			return false;
		}

		private bool ValidateTerritoriesState(object state)
		{
			return false;
		}

		private bool ValidateVideosState(object state)
		{
			return false;
		}

		private void Videos(object state)
		{
		}

		#endregion Private Methods
	}
}
